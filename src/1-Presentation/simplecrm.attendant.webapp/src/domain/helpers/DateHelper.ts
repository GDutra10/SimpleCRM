export class DateHelper{
    static locale: string = "en-US";
    
    static Now() : Date{
        return new Date();
    }
    
    static NowString() : string{
        return DateHelper.Now().toLocaleString(DateHelper.locale);
    }
    
    static ToString(date: Date) : string {
        return date.toLocaleString(DateHelper.locale);
    }
}