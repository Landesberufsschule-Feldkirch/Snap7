FUNCTION_BLOCK TestProgramm
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
    SchrittOfentuere: INT;
	Warten: TON;
END_VAR

(*  *********************************************************** *)
(*  LAP 2010/3 Ofenstürsteuerung                                *)
(*  *********************************************************** *)



CASE #SchrittOfentuere OF
    0:  // Ofentüre ist geschlossen
        "DigOutput".P1 := FALSE;
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := FALSE;
        #Warten(IN := FALSE,
                PT := T#6S); // Timer sind halt so
        IF "DigInput".S2 THEN
            #SchrittOfentuere := 1;
        END_IF;
        
    1:  // Ofentüre öffnet
        "DigOutput".P1 := FALSE;
        "DigOutput".Q1 := TRUE;
        "DigOutput".Q2 := FALSE;
        IF "DigInput".B1 THEN
            #SchrittOfentuere := 2;
        END_IF;
        IF NOT "DigInput".S1 THEN
            #SchrittOfentuere := 10;
        END_IF;
        
    2: // Ofentüre bleibt für 6" offen
        "DigOutput".P1 := FALSE;
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := FALSE;
        #Warten(IN := TRUE,
                PT := T#6S);
        IF #Warten.Q THEN
            #SchrittOfentuere := 3;
        END_IF;
        
        3: // Ofentüre fährt wieder zu
            "DigOutput".P1 := TRUE;
            "DigOutput".Q1 := FALSE;
            "DigOutput".Q2 := "DigInput".B3;
            IF "DigInput".B2 THEN
                #SchrittOfentuere := 0;
            END_IF;
            IF NOT "DigInput".S1 THEN
                #SchrittOfentuere := 10;
            END_IF;
            
        10: // Es wurde "HALT" gedrückt
            "DigOutput".P1 := FALSE;
            "DigOutput".Q1 := FALSE;
            "DigOutput".Q2 := FALSE;
            IF "DigInput".S2 THEN
                #SchrittOfentuere := 1;
            END_IF;
            IF "DigInput".S3 THEN
                #SchrittOfentuere := 3;
            END_IF;
        
    ELSE  // Unbekannter Schritt --> auf 0 setzen
        #SchrittOfentuere := 0;
END_CASE;
