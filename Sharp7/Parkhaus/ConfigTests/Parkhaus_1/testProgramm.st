FUNCTION_BLOCK "DeleteMe"
{ S7_Optimized_Access := 'TRUE' }
VERSION : 0.1
   VAR 
      WartezeitEingang : Bool;
      AktuelleParkreihe : Int;
      Reihe : Int;
      ParkgarageFreiePlaetze : Array[0..3] of Byte;
      Wartezeit {OriginalPartName := 'IEC_TIMER'; LibVersion := '1.0'} : TON_TIME;
   END_VAR

   VAR_TEMP 
      FahrzeugReiheEinlesen : Word;
      Ultraschallsensoren : Byte;
      Bitmuster : Byte;
      Bitposition : Int;
   END_VAR

   VAR CONSTANT 
      Eins : Byte := 1;
   END_VAR


BEGIN
	"DigOutput".Pr1 := #Reihe = 0;
	"DigOutput".Pr2 := #Reihe = 1;
	"DigOutput".Pr3 := #Reihe = 2;
	"DigOutput".Pr4 := #Reihe = 3;
	        
	#WartezeitEingang := TRUE;
	
	IF #Wartezeit.Q THEN
	    
	    
	    #FahrzeugReiheEinlesen := 0;
	    #Ultraschallsensoren := PEEK(area := 16#84, dbNumber := 2, byteOffset := 0);
	    
	    FOR #Bitposition := 0 TO 7 DO
	        #Bitmuster := SHL(IN := #Eins, N := #Bitposition);
	        
	        IF (#Ultraschallsensoren AND #Bitmuster) = #Bitmuster THEN
	            #FahrzeugReiheEinlesen := #FahrzeugReiheEinlesen + 1;
	        END_IF;
	        
	    END_FOR;
	    
	    #ParkgarageFreiePlaetze[#Reihe] := 8 - #FahrzeugReiheEinlesen;
	    
	    #WartezeitEingang := FALSE;
	    #Reihe += 1;
	END_IF;
	
	IF #Reihe > 3 THEN
	    #Reihe := 0;
	END_IF;
	    
	
	#Wartezeit(IN := #WartezeitEingang, PT := t#400ms);
	
	"AnOutput".Anzahl := #ParkgarageFreiePlaetze[0] + #ParkgarageFreiePlaetze[1] + #ParkgarageFreiePlaetze[2] + #ParkgarageFreiePlaetze[3];
END_FUNCTION_BLOCK

