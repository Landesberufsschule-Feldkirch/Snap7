FUNCTION_BLOCK TestProgramm
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
    SchrittSilosteuerung: Int;
	StoerungAkiv: Bool;
    SiloFuellen: Bool;
END_VAR
CONSTANT
    90Prozent: Int = 24883; // 100% = 27648
    80Prozent: Int = 22118; // 100% = 27648
    30Prozent: Int = 8304;  // 100% = 27648
END_VAR

(*  *********************************************************** *)
(*  LAP2018/1 Silosteuerung                                     *)
(*  *********************************************************** *)

IF (NOT "DigInput".F1) OR (NOT "DigInput".F2) OR (NOT "DigInput".S2) THEN
    #SchrittSilosteuerung := 100;
END_IF;

CASE #SchrittSilosteuerung OF
    0:  // Betriebsbereit
        "DigOutput".P1 := "Clock_1Hz";
        "DigOutput".P2 := FALSE;
        "DigOutput".Y1 := FALSE;
        "DigOutput".Q1 := FALSE;
        #SiloFuellen := FALSE;
        
        IF "DigInput".S1 THEN
            #SchrittSilosteuerung := 1;
        END_IF;
        
    1: //Anlage in Betrieb
        "DigOutput".P1 := TRUE;
        "DigOutput".P2 := FALSE;
        "DigOutput".Y1 := FALSE;
        "DigOutput".Q1 := FALSE;
        #SiloFuellen := TRUE;
        
        IF "DigInput".B1 AND (NOT "DigInput".B2) AND "AnInput".SiloFüllstand > #"30Prozent" THEN
            #SchrittSilosteuerung := 2;
        END_IF;
        
    2: // Wagen füllen        
        "DigOutput".P1 := TRUE;
        "DigOutput".P2 := FALSE;
        "DigOutput".Y1 := TRUE;
        "DigOutput".Q1 := TRUE;
        
        IF (NOT "DigInput".B1) OR "DigInput".B2 THEN
            #SchrittSilosteuerung := 1;
        END_IF;
        
        
    100: // Störung liegt an
        "DigOutput".Q1 := FALSE;
        "DigOutput".Y1 := FALSE;
        "DigOutput".P1 := FALSE;
        #SiloFuellen := FALSE;
        
        "DigOutput".P2 := (NOT "DigInput".F1) OR (NOT "DigInput".F2) OR (NOT "DigInput".S2) OR (#StoerungAkiv AND "Clock_1Hz");
       
        
        IF (NOT "DigInput".F1) OR (NOT "DigInput".F2) OR (NOT "DigInput".S2) THEN
            #StoerungAkiv := TRUE;
        END_IF;
        
        IF "DigInput".F1 AND "DigInput".F2 AND "DigInput".S2 AND "DigInput".S3 THEN
            #StoerungAkiv := FALSE;
        END_IF;
        
        IF NOT #StoerungAkiv THEN
            #SchrittSilosteuerung := 0;
        END_IF;
        
    ELSE  // Unbekannter Schritt --> auf was sicheres wechseln
        #SchrittSilosteuerung := 0;
END_CASE;


IF #SiloFuellen THEN
    IF "DigOutput".Q2 THEN
        IF "AnInput".SiloFüllstand > #"90Prozent" THEN  "DigOutput".Q2 := FALSE;    END_IF;
    ELSE
        IF "AnInput".SiloFüllstand < #"80Prozent" THEN  "DigOutput".Q2 := TRUE;     END_IF;
    END_IF;
ELSE
    "DigOutput".Q2 := FALSE;
END_IF;