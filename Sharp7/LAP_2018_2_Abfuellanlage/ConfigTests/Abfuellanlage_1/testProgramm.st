FUNCTION_BLOCK TestProgramm
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
    SchrittAbfuellanlage: Int;
    StoerungAkiv: BOOL;
    ZyklusStoppen: BOOL;
	Vereinzeln: TON;
    Fuellen: TON;
    Nachlauf: TON;
END_VAR
CONST
    30Prozent: Int = 8304;  //100% = 27648
END_VAR

(*  *********************************************************** *)
(*  LAP 2018/2 Abfüllanlage                                     *)
(*  *********************************************************** *)



IF NOT "DigInput".F1 THEN
    #SchrittAbfuellanlage := 100;
END_IF;

IF NOT "DigInput".S2 THEN
    #ZyklusStoppen := TRUE;
END_IF;

CASE #SchrittAbfuellanlage OF
    0: // Aus
        "DigOutput".K1 := FALSE;
        "DigOutput".K2 := FALSE;
        "DigOutput".P1 := FALSE;
        "DigOutput".P2 := FALSE;
        "DigOutput".Q1 := FALSE;
        #ZyklusStoppen := FALSE;
        
        IF "DigInput".S1 THEN
            #SchrittAbfuellanlage := 1;
        END_IF;
        
    1:  // Anlage Ein        
        "DigOutput".K1 := FALSE;
        "DigOutput".K2 := FALSE;
        "DigOutput".P1 := TRUE;
        "DigOutput".P2 := FALSE;
        "DigOutput".Q1 := FALSE;
        
        #Vereinzeln(IN := FALSE,
                    PT := T#2s);
        #Fuellen(IN := FALSE,
                 PT := T#6s);
        #Nachlauf(IN := FALSE,
                  PT := T#10s);
        
        IF NOT "DigInput".S2 OR #ZyklusStoppen THEN
            #SchrittAbfuellanlage := 0;
        END_IF;
        IF "DigInput".S3 AND "AnInput".Pegel > #"30Prozent" THEN
            #SchrittAbfuellanlage := 2;
        END_IF;
        
    2: // Flasche Vereinzeln
        "DigOutput".K2 := TRUE;
        "DigOutput".P1 := "Clock_1Hz" OR (NOT #ZyklusStoppen);
        
        #Vereinzeln(IN := TRUE,
                    PT := T#2s);
        
        IF #Vereinzeln.Q THEN
            #SchrittAbfuellanlage := 3;
        END_IF;
        
    3: // Förderband ein
        "DigOutput".K1 := FALSE;
        "DigOutput".K2 := FALSE;
        "DigOutput".P1 := "Clock_1Hz" OR (NOT #ZyklusStoppen);
        "DigOutput".Q1 := true;
        

        IF "DigInput".B1 THEN
            #SchrittAbfuellanlage := 4;
        END_IF;
        
    4: // Füllen
        "DigOutput".K1 := TRUE;
        "DigOutput".P1 := "Clock_1Hz" OR (NOT #ZyklusStoppen);
        "DigOutput".Q1 := FALSE;
        
        #Fuellen(IN := TRUE,
                 PT := T#6s);
        
        IF #Fuellen.Q THEN
            #SchrittAbfuellanlage := 5;
        END_IF;
        
    5: // Nachlauf
        "DigOutput".K1 := FALSE;
        "DigOutput".P1 := "Clock_1Hz" OR (NOT #ZyklusStoppen);
        "DigOutput".Q1 := TRUE;
        
        #Nachlauf(IN := TRUE,
                  PT := T#10s);

        IF #Nachlauf.Q THEN
            #SchrittAbfuellanlage := 1;
        END_IF;
        
    100: // Störung liegt an
        "DigOutput".K1 := FALSE;
        "DigOutput".K2 := FALSE;
        "DigOutput".P1 := FALSE;
        "DigOutput".P2 := (NOT "DigInput".F1) OR (#StoerungAkiv AND "Clock_1Hz");
        "DigOutput".Q1 := FALSE;
        #ZyklusStoppen := FALSE;
        
        IF NOT "DigInput".F1 THEN
            #StoerungAkiv := TRUE;
        END_IF;
        IF "DigInput".F1 AND "DigInput".S4 THEN
            #StoerungAkiv := FALSE;
        END_IF;
        IF NOT #StoerungAkiv THEN
            #SchrittAbfuellanlage := 0;
        END_IF;
        
    ELSE  // Unbekannter Schritt --> auf was sicheres wechseln
        #SchrittAbfuellanlage := 0;
END_CASE;
