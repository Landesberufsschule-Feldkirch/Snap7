FUNCTION_BLOCK TestProgramm
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
	VorwahlAus: Bool;
	VorwahlEin: Bool;
	SchrittLinearachse: Int;
	Zykluszeit: TON;
END_VAR

(*  ****************************************************************************************************** *)
(*  Linearachse: Motorschaltungen 2 - Direktes Einschalten mit automatischer Reversierung Version 1        *)
(*  ****************************************************************************************************** *)


IF "DigInput".S1 THEN
    #VorwahlEin := TRUE;
END_IF;

IF NOT "DigInput".S2 THEN
    #VorwahlAus := TRUE;
END_IF;



CASE #SchrittLinearachse OF
    0:  // Alles Aus
        "DigOutput".P4 := FALSE;
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := FALSE;
        
        #Zykluszeit(IN := FALSE,
                    PT := T#10s);
        
        IF #VorwahlAus THEN
            #VorwahlAus := FALSE;
            #VorwahlEin := FALSE;
        END_IF;
        
        IF #VorwahlEin THEN
            #SchrittLinearachse := 1;
        END_IF;
        
    1:  // Motor fährt nach rechts
        "DigOutput".P4 := (NOT #VorwahlAus) OR "Clock_1Hz";
        "DigOutput".Q1 := TRUE;
        "DigOutput".Q2 := FALSE;
        
        #Zykluszeit(IN := TRUE,
                    PT := T#10s);
        
        IF #Zykluszeit.ET > T#2s THEN
            #SchrittLinearachse := 2;
        END_IF;
        
    2: // Motor bleibt stehen
        "DigOutput".P4 := (NOT #VorwahlAus) OR "Clock_1Hz";
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := FALSE;
        
        #Zykluszeit(IN := TRUE,
                    PT := T#10s);
        
        IF #Zykluszeit.ET > T#3s THEN
            #SchrittLinearachse := 3;
        END_IF;
        
    3: // Motor fährt nach links
        
        "DigOutput".P4 := (NOT #VorwahlAus) OR "Clock_1Hz";
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := TRUE;
        
        #Zykluszeit(IN := TRUE,
                    PT := T#10s);
        
        IF #Zykluszeit.ET > T#5s THEN
            #SchrittLinearachse := 4;
        END_IF;
        
    4: // Motor wartet links    
        
        "DigOutput".P4 := NOT #VorwahlAus;
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := FALSE;
        
        #Zykluszeit(IN := TRUE,
                    PT := T#10s);
        
        IF #Zykluszeit.ET > T#6s THEN
            #SchrittLinearachse := 0;
        END_IF;
        
    ELSE  // Statement section ELSE
        #SchrittLinearachse := 0;
END_CASE;
