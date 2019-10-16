import { Status } from '../_enums/status.enum';

export interface RequestResult {
  status: Status,
  data?: any,
  messages: string[]
}
