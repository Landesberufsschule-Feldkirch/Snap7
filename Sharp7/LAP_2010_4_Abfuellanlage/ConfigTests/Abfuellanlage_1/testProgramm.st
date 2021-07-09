FUNCTION_BLOCK DeleteMe

VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
    SchrittBehaelterFuellen: INT;
	Impuls: TON;
    DoseFuellzeit: TON;
    DoseWeiterbefoerderzeit: TON;
    StoerungBehaelterLeer: BOOL;
END_VAR


(*  *********************************************************** *)
(*  LAP 2010/4 Abfüllanlage                                     *)
(*  *********************************************************** *)

CASE SchrittBehaelterFuellen OF
    0:  // Warten
        SimDA.K1 := FALSE;
        SimDA.K2 := FALSE;
        SimDA.Q1 := FALSE;
        Impuls(IN := FALSE, PT := T#1S);
        DoseFuellzeit(IN := FALSE, PT := T#3S);
        DoseWeiterbefoerderzeit(IN := FALSE, PT := T#8S);
        IF SimDI.S2 AND SimDI.B1 AND (NOT StoerungBehaelterLeer) THEN SchrittBehaelterFuellen := 1; END_IF;
        
    1: // Impuls für Vereinzelner erzeugen
        SimDA.K1 := TRUE;
        SimDA.K2 := FALSE;
        SimDA.Q1 := FALSE;
        Impuls(IN := TRUE, PT := T#1S);
        IF Impuls.Q THEN SchrittBehaelterFuellen := 2; END_IF;
        
    2: // Förderband laufen lassen 
        SimDA.K1 := FALSE;
        SimDA.K2 := FALSE;
        SimDA.Q1 := TRUE;
        IF SimDI.B2 THEN SchrittBehaelterFuellen := 3; END_IF;
        
    3: // Dose füllen
        SimDA.K1 := FALSE;
        SimDA.K2 := TRUE;
        SimDA.Q1 := FALSE;
        DoseFuellzeit(IN := TRUE, PT := T#3S);
        IF DoseFuellzeit.Q THEN SchrittBehaelterFuellen := 4;END_IF;
        
    4: // Dose weiterbefördern
        SimDA.K1 := FALSE;
        SimDA.K2 := FALSE;
        SimDA.Q1 := TRUE;
        DoseWeiterbefoerderzeit(IN := TRUE, PT := T#8S);
        IF DoseWeiterbefoerderzeit.Q THEN SchrittBehaelterFuellen := 0; END_IF;
                
    ELSE  // Unbekannter Schritt --> auf 0 setzen
        SchrittBehaelterFuellen := 0;
END_CASE;

IF NOT SimDI.B1           THEN    StoerungBehaelterLeer := TRUE;     END_IF;
IF SimDI.B1 AND SimDI.S1  THEN    StoerungBehaelterLeer := FALSE;    END_IF;

SimDA.P1 := (NOT SimDI.B1) OR (StoerungBehaelterLeer AND Blinker.Clock_1Hz);