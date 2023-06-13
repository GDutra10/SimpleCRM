import {EnvironmentHelper} from "../../domain/helpers/EnvironmentHelper";
import {DateHelper} from "../../domain/helpers/DateHelper";

export class Logger{
    static canLog() : boolean{
        return EnvironmentHelper.isDev();
    }
    
    static logDebug(message: string){
        if (Logger.canLog()){
            console.log(`%c${DateHelper.NowString()} | DEBUG | ${message}`, 'color: #87CEFA');
        }
    }
    
    static logInfo (message: string) {
        if (Logger.canLog()){
            console.log(`${DateHelper.NowString()} | INFO | ${message}`);
        }
    }
    
    static logWarn(message: string) {
        if (Logger.canLog()){
            console.log(`%c${DateHelper.NowString()} | WARN | ${message}`, 'color: #FFBF00');
        }
    }
    
    static logError(message: string){
        if (Logger.canLog()){
            console.log(`%c${DateHelper.NowString()} | ERROR | ${message}`, 'color: #B22222');
        }   
    }
}