﻿namespace ETModel
{
    public enum NumericType
    {

        #region Manage 1001 管理，每升1级加1，影响体力,精神力,要钱加成
        Manage = 1001,                                
        ManageBase = Manage * 10 + 1,
        ManageAdd = Manage * 10 + 2,

        MaxManage = 1002,
        MaxManageBase = MaxManage * 10 + 1,
        #endregion

        #region Valuation 2001 计价，每升1级加1.2，影响气血量，耐力，自带钱数
        Valuation = 2001,                            
        ValuationBase = Valuation * 10 + 1,
        ValuationAdd = Valuation * 10 + 2,
        ValuationPct = Valuation * 10 + 3,
        ValuationFinalAdd = Valuation * 10 + 4,
        ValuationFinalPct = Valuation * 10 + 5,

        MaxValuation = 2001,
        MaxValuationBase = MaxValuation * 10 + 1,
        MaxValuationAdd = MaxValuation * 10 + 2,
        MaxValuationPct = MaxValuation * 10 + 3,
        MaxValuationFinalAdd = MaxValuation * 10 + 4,
        MaxValuationFinalPct = MaxValuation * 10 + 5,

  
        #endregion

        #region Measure 3001 计量，每升1级加1，影响速度，要钱速度
        Measure = 3001,                             
        MeasureBase = Measure * 10 + 1,
        MeasureAdd = Measure * 10 + 2,
        MeasurePct = Measure * 10 + 3,
        MeasureFinalAdd = Measure * 10 + 4,
        MeasureFinalPct = Measure * 10 + 5,

        MaxMeasure = 3001,
        MaxMeasureBase = MaxMeasure * 10 + 1,
        MaxMeasureAdd = MaxMeasure * 10 + 2,
      
        #endregion

        #region Case 4001 案例，每升1级加1.4，影响攻击，要钱数量 
        Case = 4001,                               
        CaseBase = Case * 10 + 1,
        CaseAdd = Case * 10 + 2,
        CasePct = Case * 10 + 3,
        CaseFinalAdd = Case * 10 + 4,
        CaseFinalPct = Case * 10 + 5,

        MaxCase = 4001,
        MaxCaseBase = MaxCase * 10 + 1,
        MaxCaseAdd = MaxCase * 10 + 2,

        #endregion

        #region Level 5001
        Max = 10000,

        Level = 5101,
        LevelBase = Level * 10 + 1,
        LevelAdd = Level * 10 + 2,

        Exp = 5102,
        ExpBase = Exp * 10 + 1,
        ExpAdd = Exp * 10 + 2,

        Coin = 5103,
        CoinBase = Coin * 10 + 1,
        CoinAdd = Coin * 10 + 2,

        //Hp = 5201,
        //HpBase = Hp * 10 + 1,
        //HpAdd = Hp * 10 + 2,
        //HpPct = Hp * 10 + 3,
        //HpFinalAdd = Hp * 10 + 4,
        //HpFinalPct = Hp * 10 + 5,

        //MaxHp = 5202,
        //MaxHpBase = MaxHp * 10 + 1,
        //MaxHpAdd = MaxHp * 10 + 2,
        //MaxHpPct = MaxHp * 10 + 3,
        //MaxHpFinalAdd = MaxHp * 10 + 4,
        //MaxHpFinalPct = MaxHp * 10 + 5,

        //Sp = 5301,
        //SpBase = Sp * 10 + 1,
        //SpAdd = Sp * 10 + 2,
        //SpPct = Sp * 10 + 3,
        //SpFinalAdd = Sp * 10 + 4,
        //SpFinalPct = Sp * 10 + 5,

        //Speed = 5302,
        //SpeedBase = Speed * 10 + 1,
        //SpeedAdd = Speed * 10 + 2,
        //SpeedPct = Speed * 10 + 3,
        //SpeedFinalAdd = Speed * 10 + 4,
        //SpeedFinalPct = Speed * 10 + 5,

        //Ap = 5401,
        //ApBase = Ap * 10 + 1,
        //ApAdd = Ap * 10 + 2,
        //ApPct = Ap * 10 + 3,
        //ApFinalAdd = Ap * 10 + 4,
        //ApFinalPct = Ap * 10 + 5,

        #endregion


    }
}
