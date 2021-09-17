FUNCTION_BLOCK DeleteMe

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

CASE SchrittOfentuere OF
    0:  // Ofentüre ist geschlossen
        SimDA.P1 := FALSE;
        SimDA.Q1 := FALSE;
        SimDA.Q2 := FALSE;
        Warten(IN := FALSE, PT := T#6S); // Timer sind halt so
        IF SimDI.S2 THEN SchrittOfentuere := 1; END_IF;
        
    1:  // Ofentüre öffnet
        SimDA.P1 := FALSE;
        SimDA.Q1 := TRUE;
        SimDA.Q2 := FALSE;
        IF SimDI.B1        THEN SchrittOfentuere := 2;    END_IF;
        IF NOT SimDI.S1    THEN SchrittOfentuere := 10;   END_IF;
        
    2: // Ofentüre bleibt für 6" offen
        SimDA.P1 := FALSE;
        SimDA.Q1 := FALSE;
        SimDA.Q2 := FALSE;
        Warten(IN := TRUE, PT := T#6S);
        IF Warten.Q THEN SchrittOfentuere := 3; END_IF;
        
    3: // Ofentüre fährt wieder zu
        SimDA.P1 := TRUE;
        SimDA.Q1 := FALSE;
        SimDA.Q2 := SimDI.B3;
        IF SimDI.B2        THEN SchrittOfentuere := 0;    END_IF;
        IF NOT SimDI.S1    THEN SchrittOfentuere := 10;   END_IF;
        
    10: // Es wurde "HALT" gedrückt
        SimDA.P1 := FALSE;
        SimDA.Q1 := FALSE;
        SimDA.Q2 := FALSE;
        IF SimDI.S2 THEN SchrittOfentuere := 1; END_IF;
        IF SimDI.S3 THEN SchrittOfentuere := 3; END_IF;
        
    ELSE  // Unbekannter Schritt --> auf 0 setzen
        SchrittOfentuere := 0;
END_CASE;