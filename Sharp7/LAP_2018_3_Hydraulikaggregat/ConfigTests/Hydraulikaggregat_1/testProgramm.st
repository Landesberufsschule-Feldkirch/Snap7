FUNCTION_BLOCK TestProgramm
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
    TasterZylinderAlt: Bool;
    StoerungOelfilter: Bool;
    SchrittHydraulik: int;
	SternDreieckAnlauf: TON;
    VerzoegerungOelstand: TON;
    VerzoegerungOelfilter: TON;
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

////////////////////////////////////
// Erweiterung "Ölkühler"
////////////////////////////////////

"DigOutput".Q4 := #SchrittHydraulik > 0 AND #SchrittHydraulik < 100 AND (NOT "DigInput".B4);
"DigOutput".P7 := "DigOutput".Q4;

////////////////////////////////////
// Erweiterung "Zylinder"
////////////////////////////////////

IF "DigOutput".P5 THEN
    IF "DigInput".S4 AND (NOT #TasterZylinderAlt) THEN
        "DigOutput".K1 := NOT "DigOutput".K1;
        "DigOutput".K2 := NOT "DigOutput".K1;
    END_IF;
ELSE
    "DigOutput".K1 := FALSE;
    "DigOutput".K2 := FALSE;
END_IF;

#TasterZylinderAlt := "DigInput".S4;

////////////////////////////////////
// Erweiterung "Ölfilter"
////////////////////////////////////

#VerzoegerungOelfilter(IN := (NOT "DigInput".B5) AND #SchrittHydraulik > 0 AND #SchrittHydraulik < 100,
                       PT := T#5s);

IF #VerzoegerungOelfilter.Q THEN
    #StoerungOelfilter := TRUE;
END_IF;

"DigOutput".P8 := #StoerungOelfilter OR ((NOT "DigInput".B5) AND "Clock_1Hz");

IF "DigInput".B5 AND "DigInput".S3 THEN
    #StoerungOelfilter := FALSE;
END_IF;




