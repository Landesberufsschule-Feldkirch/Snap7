FUNCTION_BLOCK TestProgramm
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

CASE #SchrittBehaelterFuellen OF
    0:  // Warten
        "DigOutput".K1 := FALSE;
        "DigOutput".K2 := FALSE;
        "DigOutput".Q1 := FALSE;
        #Impuls(IN := FALSE, PT := T#1S);
        #DoseFuellzeit(IN := FALSE, PT := T#3S);
        #DoseWeiterbefoerderzeit(IN := FALSE, PT := T#8S);
        IF "DigInput".S2 AND "DigInput".B1 AND (NOT #StoerungBehaelterLeer) THEN #SchrittBehaelterFuellen := 1; END_IF;
        
    1: // Impuls für Vereinzelner erzeugen
        "DigOutput".K1 := TRUE;
        "DigOutput".K2 := FALSE;
        "DigOutput".Q1 := FALSE;
        #Impuls(IN := TRUE, PT := T#1S);
        IF #Impuls.Q THEN #SchrittBehaelterFuellen := 2; END_IF;
        
    2: // Förderband laufen lassen 
        "DigOutput".K1 := FALSE;
        "DigOutput".K2 := FALSE;
        "DigOutput".Q1 := TRUE;
        IF "DigInput".B2 THEN #SchrittBehaelterFuellen := 3; END_IF;
        
    3: // Dose füllen
        "DigOutput".K1 := FALSE;
        "DigOutput".K2 := TRUE;
        "DigOutput".Q1 := FALSE;
        #DoseFuellzeit(IN := TRUE, PT := T#3S);
        IF #DoseFuellzeit.Q THEN #SchrittBehaelterFuellen := 4;END_IF;
        
    4: // Dose weiterbefördern
        "DigOutput".K1 := FALSE;
        "DigOutput".K2 := FALSE;
        "DigOutput".Q1 := TRUE;
        #DoseWeiterbefoerderzeit(IN := TRUE, PT := T#8S);
        IF #DoseWeiterbefoerderzeit.Q THEN #SchrittBehaelterFuellen := 0; END_IF;
                
    ELSE  // Unbekannter Schritt --> auf 0 setzen
        #SchrittBehaelterFuellen := 0;
END_CASE;

IF NOT "DigInput".B1                THEN    #StoerungBehaelterLeer := TRUE;     END_IF;
IF "DigInput".B1 AND "DigInput".S1  THEN    #StoerungBehaelterLeer := FALSE;    END_IF;

"DigOutput".P1 := (NOT "DigInput".B1) OR (#StoerungBehaelterLeer AND "Clock_1Hz");
