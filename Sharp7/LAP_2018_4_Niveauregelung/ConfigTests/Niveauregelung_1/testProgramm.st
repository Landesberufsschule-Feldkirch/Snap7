FUNCTION_BLOCK TestProgramm
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
    SchrittNiveauRegelung: Int;
	StoerungAktiv: BOOL;
	StoerungFuellstandQuittiert: BOOL;
	FuellstandObenUeberschritten: TON;
	FuellstandObenUnterschritten: TON;
	FuellstandMitteUnterschritten: TON;
END_VAR

(*  *********************************************************** *)
(*  LAP 2018/4 Niveauregelung                                   *)
(*  *********************************************************** *)

IF #StoerungFuellstandQuittiert THEN
    IF "DigInput".B1 THEN
        #StoerungFuellstandQuittiert := FALSE;
    END_IF;
ELSE
    IF (NOT "DigInput".F1) OR (NOT "DigInput".F1) OR (NOT "DigInput".B1) THEN
        #SchrittNiveauRegelung := 100;
    END_IF;
END_IF;

CASE #SchrittNiveauRegelung OF
    0:  // Aus
        "DigOutput".P1 := FALSE;
        "DigOutput".P2 := FALSE;
        "DigOutput".P3 := FALSE;
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := FALSE;
        
        IF "DigInput".S1 THEN
            #SchrittNiveauRegelung := 1;
        END_IF;
        
        #FuellstandObenUeberschritten(IN := FALSE,
                                      PT := T#1s);
        #FuellstandMitteUnterschritten(IN := FALSE,
                                       PT := T#2s);
        #FuellstandObenUnterschritten(IN := FALSE,
                                      PT := T#2s);
        
    1:  // Betrieb; beiden Pumpen aus
        "DigOutput".P1 := FALSE;
        "DigOutput".P2 := TRUE;
        "DigOutput".P3 := FALSE;
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := FALSE;
        
        #FuellstandObenUnterschritten(IN := "DigInput".B3,
                                      PT := T#2s);
        
        IF NOT "DigInput".S2 THEN
            #SchrittNiveauRegelung := 0;
        END_IF;
        
        IF #FuellstandObenUnterschritten.Q THEN
            #SchrittNiveauRegelung := 2;
        END_IF;
        
    2: // Pumpe 1 in Betrieb
        "DigOutput".P1 := FALSE;
        "DigOutput".P2 := TRUE;
        "DigOutput".P3 := FALSE;
        "DigOutput".Q1 := TRUE;
        "DigOutput".Q2 := FALSE;
        
        #FuellstandObenUeberschritten(IN := NOT "DigInput".B3,
                                      PT := T#1s);
        #FuellstandObenUnterschritten(IN := "DigInput".B3,
                                      PT := T#2s);
        #FuellstandMitteUnterschritten(IN := NOT "DigInput".B2,
                                       PT := T#2s);
        
        
        IF NOT "DigInput".S2 THEN
            #SchrittNiveauRegelung := 0;
        END_IF;
        
        IF #FuellstandObenUeberschritten.Q THEN
            #SchrittNiveauRegelung := 1;
        END_IF;
        
        IF #FuellstandMitteUnterschritten.Q THEN
            #SchrittNiveauRegelung := 3;
        END_IF;
        
    3: // Pumpen 1 und 2 in Betrieb
        "DigOutput".P1 := FALSE;
        "DigOutput".P2 := TRUE;
        "DigOutput".P3 := FALSE;
        "DigOutput".Q1 := TRUE;
        "DigOutput".Q2 := TRUE;
        
        #FuellstandObenUeberschritten(IN := NOT "DigInput".B3,
                                      PT := T#1s);
        IF NOT "DigInput".S2 THEN
            #SchrittNiveauRegelung := 0;
        END_IF;
        
        IF #FuellstandObenUeberschritten.Q THEN
            #SchrittNiveauRegelung := 1;
        END_IF;
        
        
    100:  // Störung aktiv
        "DigOutput".P1 := (NOT "DigInput".F1) OR (NOT "DigInput".F1) OR (NOT "DigInput".B1) OR (#StoerungAktiv AND "Clock_1Hz");
        "DigOutput".P2 := FALSE;
        "DigOutput".P3 := FALSE;
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := FALSE;
        
        IF "DigInput".S3 THEN
            #StoerungFuellstandQuittiert := TRUE;
        END_IF;
        
        IF (NOT "DigInput".F1) OR (NOT "DigInput".F1) OR (NOT "DigInput".B1) THEN
            #StoerungAktiv := TRUE;
        END_IF;
        
        IF "DigInput".F1 AND "DigInput".F1 AND "DigInput".S3 THEN
            #StoerungAktiv := FALSE;
        END_IF;
        
        IF NOT #StoerungAktiv THEN
            #SchrittNiveauRegelung := 0;
        END_IF;
        
        
    ELSE  // Unbekannter Schritt --> auf was sicheres wechseln
        #SchrittNiveauRegelung := 0;
END_CASE;

