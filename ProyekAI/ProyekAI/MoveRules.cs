using System;
using System.Collections.Generic;
using System.Text;

namespace ProyekAI
{
    public enum MoveRules
    {
        StraightLine,
        LegalDistance,
        AvoidsOwnPieces,
        PushesLessThan2Pieces,
        DoesNotPushWhilePassive,
        MatchesPassiveMoveWhileAggressive
    }
}