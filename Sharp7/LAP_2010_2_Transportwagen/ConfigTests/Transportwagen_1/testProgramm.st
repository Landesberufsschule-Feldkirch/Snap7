FUNCTION_BLOCK DeleteMe

VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
	SchrittTransportwagen: INT;
	BeladenAbwarten: TON;
END_VAR


(*  *********************************************************** *)
(*  LAP 2010/2 Transportwagen                                   *)
(*  *********************************************************** *)

IF (NOT SimDI.F1) OR (NOT SimDI.S2) THEN
    SchrittTransportwagen := 100;
END_IF;

CASE SchrittTransportwagen OF
    0:  // Transportwagen steht links (Punkt A)
        SimDA.P1 := FALSE;
        SimDA.Q1 := FALSE;
        SimDA.Q2 := FALSE;
        BeladenAbwarten(IN := FALSE, PT := T#7S); 
        IF SimDI.S1 THEN SchrittTransportwagen := 1; END_IF;
        
    1:  // Transportwagen fährt nach rechts (Punkt B)
        SimDA.P1 := FALSE;
        SimDA.Q1 := FALSE;
        SimDA.Q2 := TRUE;
        IF SimDI.B2 THEN SchrittTransportwagen := 2; END_IF;
        
    2: // Transportwagen wartet 7 Sekunden
        SimDA.Q1 := FALSE;
        SimDA.Q2 := FALSE;
        BeladenAbwarten(IN := TRUE, PT := T#7S);
        IF BeladenAbwarten.Q THEN SchrittTransportwagen := 3; END_IF;
        
    3: // Transportwagen fährt zurück links (Punkt A)
        SimDA.P1 := FALSE;
        SimDA.Q1 := TRUE;
        SimDA.Q2 := FALSE;
        IF SimDI.B1 THEN SchrittTransportwagen := 0; END_IF;
        
    100: // Störung aktiv
        SimDA.P1 := (NOT SimDI.F1) OR (NOT SimDI.S2) OR Blinker.Clock_1Hz;
        SimDA.Q1 := FALSE;
        SimDA.Q2 := FALSE;
        IF SimDI.F1 AND SimDI.S2 AND SimDI.S3 THEN SchrittTransportwagen := 3; END_IF;
        
        
    ELSE  // Unbekannter Schritt --> auf was sicheres wechseln
        SchrittTransportwagen := 0;
END_CASE;