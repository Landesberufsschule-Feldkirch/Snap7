FUNCTION "FahrzeugReiheEinlesen" : Word
{ S7_Optimized_Access := 'TRUE' }
VERSION : 0.1
   VAR_TEMP 
      Ultraschallsensoren : Byte;
      Bitposition : Int;
      Bitmuster : Byte;
   END_VAR

   VAR CONSTANT 
      Eins : Byte := 1;
   END_VAR


BEGIN
	#FahrzeugReiheEinlesen := 0;
	#Ultraschallsensoren := PEEK(area := 16#84, dbNumber := 2, byteOffset := 0);
	
	FOR #Bitposition := 0 TO 7 BY 1 DO
	    #Bitmuster := SHL(IN := #Eins, N := #Bitposition);
	    
	    IF (#Ultraschallsensoren AND #Bitmuster) = #Bitmuster THEN
	        #FahrzeugReiheEinlesen := #FahrzeugReiheEinlesen + 1;
	    END_IF;
	    
	END_FOR;
END_FUNCTION


FUNCTION_BLOCK "DeleteMe"
{ S7_Optimized_Access := 'TRUE' }
VERSION : 0.1
   VAR 
      WartezeitEingang { ExternalAccessible := 'False'; ExternalVisible := 'False'; ExternalWritable := 'False'} : Bool;
      AktuelleParkreihe { ExternalAccessible := 'False'; ExternalVisible := 'False'; ExternalWritable := 'False'} : Int;
      Reihe : Int;
      ParkgarageFreiePlaetze : Array[0..3] of Word;
      Wartezeit {OriginalPartName := 'IEC_TIMER'; LibVersion := '1.0'} : TON_TIME;
   END_VAR


BEGIN
	CASE #Reihe OF
	    0:  // 
	        "DigOutput".Pr1 := TRUE;
	        "DigOutput".Pr2 := FALSE;
	        "DigOutput".Pr3 := FALSE;
	        "DigOutput".Pr4 := FALSE;
	        
	        #WartezeitEingang := TRUE;
	        
	        IF #Wartezeit.Q THEN
	            #Reihe := 1;
	            #WartezeitEingang := FALSE;
	            #ParkgarageFreiePlaetze[0] := 8 - "FahrzeugReiheEinlesen"();
	        END_IF;
	        
	    1:  // 
	        "DigOutput".Pr1 := FALSE;
	        "DigOutput".Pr2 := TRUE;
	        "DigOutput".Pr3 := FALSE;
	        "DigOutput".Pr4 := FALSE;
	        
	        #WartezeitEingang := TRUE;
	        
	        IF #Wartezeit.Q THEN
	            #WartezeitEingang := false;
	            #ParkgarageFreiePlaetze[1] := 8 - "FahrzeugReiheEinlesen"();
	            #Reihe := 2;
	        END_IF;
	        
	    2:  // 
	        "DigOutput".Pr1 := FALSE;
	        "DigOutput".Pr2 := FALSE;
	        "DigOutput".Pr3 := TRUE;
	        "DigOutput".Pr4 := FALSE;
	        
	        #WartezeitEingang := TRUE;
	        
	        IF #Wartezeit.Q THEN
	            #Reihe := 3;
	            #WartezeitEingang := FALSE;
	            #ParkgarageFreiePlaetze[2] := 8 - "FahrzeugReiheEinlesen"();
	        END_IF;
	        
	    3:  // 
	        "DigOutput".Pr1 := FALSE;
	        "DigOutput".Pr2 := FALSE;
	        "DigOutput".Pr3 := FALSE;
	        "DigOutput".Pr4 := TRUE;
	        
	        #WartezeitEingang := TRUE;
	        
	        IF #Wartezeit.Q THEN
	            #Reihe := 0;
	            #WartezeitEingang := FALSE;
	            #ParkgarageFreiePlaetze[3] := 8 - "FahrzeugReiheEinlesen"();
	        END_IF;
	        
	    ELSE  // Statement section ELSE
	        #Reihe := 0;
	END_CASE;
	
	#Wartezeit(IN := #WartezeitEingang, PT := t#400ms);
	
	"AnOutput".Anzahl :=  #ParkgarageFreiePlaetze[0] + #ParkgarageFreiePlaetze[1] + #ParkgarageFreiePlaetze[2] + #ParkgarageFreiePlaetze[3];
END_FUNCTION_BLOCK