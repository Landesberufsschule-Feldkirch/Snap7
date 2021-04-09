FUNCTION_BLOCK TestProgramm
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
    SchrittTransportwagen: Int;
	BeladenAbwarten: TON;
END_VAR

(*  *********************************************************** *)
(*  LAP 2010/2 Transportwagen                                   *)
(*  *********************************************************** *)

IF (NOT "DigInput".F1) OR (NOT "DigInput".S2) THEN
    #SchrittTransportwagen := 100;
END_IF;

CASE #SchrittTransportwagen OF
    0:  // Transportwagen steht links (Punkt A)
        "DigOutput".P1 := FALSE;
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := FALSE;
        #BeladenAbwarten(IN := FALSE,
                         PT := T#7s); // Timer sind halt so
        IF "DigInput".S1 THEN
            #SchrittTransportwagen := 1;
        END_IF;
        
    1:  // Transportwagen fährt nach rechts (Punkt B)
        "DigOutput".P1 := FALSE;
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := TRUE;
        IF "DigInput".B2 THEN
            #SchrittTransportwagen := 2;
        END_IF;
        
    2: // Transportwagen wartet 7 Sekunden
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := FALSE;
        #BeladenAbwarten(IN := TRUE,
                         PT := T#7s);
        IF #BeladenAbwarten.Q THEN
            #SchrittTransportwagen := 3;
        END_IF;
        
    3: // Transportwagen fährt zurück links (Punkt A)
        "DigOutput".P1 := FALSE;
        "DigOutput".Q1 := TRUE;
        "DigOutput".Q2 := FALSE;
        IF "DigInput".B1 THEN
            #SchrittTransportwagen := 0;
        END_IF;
        
    100: // Störung aktiv
        "DigOutput".P1 := (NOT "DigInput".F1) OR (NOT "DigInput".S2) OR "Clock_1Hz";
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := FALSE;
        IF "DigInput".F1 AND "DigInput".S2 AND "DigInput".S3 THEN
            #SchrittTransportwagen := 3;
        END_IF;
        
        
    ELSE  // Unbekannter Schritt --> auf was sicheres wechseln
        #SchrittTransportwagen := 0;
END_CASE;
