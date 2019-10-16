import { InjectionToken } from '@angular/core';
export let SESSION_CONFIG = new InjectionToken('session.config');

export interface ISessionConfig {
  isRequesting: Array<boolean>;
  isAuth: boolean;

}

export const SessionConfig: ISessionConfig = {
  isRequesting: [],
  isAuth: false
};
