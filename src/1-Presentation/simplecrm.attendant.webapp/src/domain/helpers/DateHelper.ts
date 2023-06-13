export class DateHelper{
    static Now() : Date{
        return new Date();
    }
    
    static NowString() : string{
        return DateHelper.Now().toLocaleString("en-US");
    }
}