FUNCTION_BLOCK TestProgramm
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
	SchrittLinearachse: Int;
	NachLinksFahren: TON;
    NachLinksZurückfahren: TON;
    NachRechtsFahren: TON;
END_VAR

(*  *************************************************************************************** *)
(*  Linearachse: Motorschaltungen 2 - Direktes Einschalten mit Reversieren Version 2        *)
(*  *************************************************************************************** *)

IF (NOT "DigInput".S9) OR (NOT "DigInput".S10) THEN
    #SchrittLinearachse := 100;
END_IF;



CASE #SchrittLinearachse OF
    0:  // Aus
        "DigOutput".P2 := FALSE;
        "DigOutput".P3 := FALSE;
        "DigOutput".P4 := FALSE;
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := FALSE;
        
        IF "DigInput".S1 THEN
            #SchrittLinearachse := 1;
            #NachRechtsFahren(IN := FALSE,
                              PT := T#2s);
        END_IF;
        
    1:  // Linearachse fährt nach rechts
        "DigOutput".P3 := FALSE;
        "DigOutput".P4 := TRUE;
        "DigOutput".Q1 := TRUE;
        "DigOutput".Q2 := FALSE;
        #NachRechtsFahren(IN := TRUE,
                          PT := T#2s);
        #NachLinksFahren(IN := FALSE,
                         PT := T#2s);
        
        IF #NachRechtsFahren.Q THEN
            #SchrittLinearachse := 2;
        END_IF;
        
    2: // Linearachse fährt nach links
        "DigOutput".P2 := FALSE;
        "DigOutput".P3 := FALSE;
        "DigOutput".P4 := TRUE;
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := TRUE;
        
        #NachLinksFahren(IN := TRUE,
                         PT := T#2s);
        
        IF #NachLinksFahren.Q THEN
            #SchrittLinearachse := 0;
        END_IF;
        
        
    100: // Störung aktiv
        "DigOutput".P2 := FALSE;
        "DigOutput".P3 := (NOT "DigInput".S9) OR (NOT "DigInput".S10) OR "Clock_1.25Hz";
        "DigOutput".P4 := FALSE;
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := FALSE;
        
        #NachLinksZurückfahren(IN := FALSE,
                               PT := T#2s);
        
        IF "DigInput".S8 AND "DigInput".S9 AND "DigInput".S10 THEN
            #SchrittLinearachse := 200;
        END_IF;
        
        
    200: // Nach links zurückfahren
        "DigOutput".P2 := "Clock_1.25Hz";
        "DigOutput".P3 := FALSE;
        "DigOutput".P4 := FALSE;
        "DigOutput".Q1 := FALSE;
        "DigOutput".Q2 := TRUE;
        
        #NachLinksZurückfahren(IN := TRUE,
                               PT := T#3s);
        
        IF #NachLinksZurückfahren.Q THEN
            #SchrittLinearachse := 0;
        END_IF;
        
    ELSE  // Unbekannter Schritt -> Reset
        #SchrittLinearachse := 0;
END_CASE;
