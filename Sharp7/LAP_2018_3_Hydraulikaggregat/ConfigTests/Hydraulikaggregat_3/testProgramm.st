FUNCTION_BLOCK TestProgramm
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
    SchrittHydraulik: int;
	SternDreieckAnlauf: TON;
    VerzoegerungOelstand: TON;
END_VAR

(*  *********************************************************** *)
(*  LAP 2018/3 Hydraulikaggregat                                *)
(*  *********************************************************** *)

"DigOutput".P3 := NOT "DigInput".F1;
"DigOutput".P4 := NOT "DigInput".B3;
"DigOutput".P6 := #VerzoegerungOelstand.Q OR ((NOT "DigInput".B1) AND "Clock_1Hz");

#VerzoegerungOelstand(IN := NOT "DigInput".B1,
                      PT := T#5s);

IF #VerzoegerungOelstand.Q OR (NOT "DigInput".B3) OR (NOT "DigInput".F1) THEN
    #SchrittHydraulik := 100;
END_IF;



CASE #SchrittHydraulik OF
    0:  // Anlage Aus
        "DigOutput".P1 := TRUE;
        "DigOutput".P2 := FALSE;
        "DigOutput".P5 := FALSE;
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := FALSE;
        "DigOutput".Q3 := FALSE;
        
        
        #SternDreieckAnlauf(PT := T#10s,
                            IN := FALSE);
        
        IF "DigInput".S1 AND "DigInput".S2 THEN
            #SchrittHydraulik := 1;
        END_IF;
        
    1: // Kompressor läuft an
        "DigOutput".P1 := FALSE;
        "DigOutput".P3 := FALSE;
        "DigOutput".Q1 := TRUE;
        
        IF "DigInput".B2 THEN
            "DigOutput".P5 := TRUE;
        END_IF;
            
        #SternDreieckAnlauf(PT := T#10s,
                            IN := TRUE);
        
        IF #SternDreieckAnlauf.ET < T#3s THEN
            "DigOutput".Q2 := TRUE;
            "DigOutput".Q3 := FALSE;
        ELSIF #SternDreieckAnlauf.ET > T#3s500ms THEN
            "DigOutput".Q2 := FALSE;
            "DigOutput".Q3 := TRUE;
        ELSE
            "DigOutput".Q2 := FALSE;
            "DigOutput".Q3 := FALSE;
        END_IF;
        
        IF "DigInput".B2 THEN
            #SchrittHydraulik := 2;
        END_IF;
        
        IF NOT "DigInput".S2 THEN
            #SchrittHydraulik := 0;
        END_IF;
        
    2: // Druck erreicht
        "DigOutput".P1 := FALSE;
        "DigOutput".P3 := FALSE;
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := FALSE;
        "DigOutput".Q3 := FALSE;
        
        IF "DigInput".B2 THEN
            "DigOutput".P5 := TRUE;
        END_IF;
        
        #SternDreieckAnlauf(PT := T#10s,
                            IN := FALSE);
        
        IF NOT "DigInput".B2 THEN
            #SchrittHydraulik := 1;
        END_IF;
        
        IF NOT "DigInput".S2 THEN
            #SchrittHydraulik := 0;
        END_IF;
        
        
    100:  // Störung
        "DigOutput".P1 := FALSE;
        "DigOutput".P5 := FALSE;
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := FALSE;
        "DigOutput".Q3 := FALSE;
        
        "DigOutput".P2 := (NOT "DigInput".F1) OR #VerzoegerungOelstand.Q OR (NOT "DigInput".B3) OR "Clock_1Hz";
        
        IF (NOT #VerzoegerungOelstand.Q) AND "DigInput".B3 AND "DigInput".F1 AND "DigInput".S3 THEN
            #SchrittHydraulik := 0;
        END_IF;
        
    ELSE  // Statement section ELSE
        #SchrittHydraulik := 0; // Auf einen gültigen Wert setzen
END_CASE;