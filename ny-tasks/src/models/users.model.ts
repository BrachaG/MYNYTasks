import { CodeTable } from "./CodeTable.model";

export class userModel {
  iUserId: number = 0;
  nvUserName: string = "";
  nvPassword: string = "";
  iUserStatus: number = 0;
  iOrganizationId: number = 0;
  iPermissionLevelId: number = 0;
  iReportedHoursTypeId: number = 0;
  iContinueContactPermissionId: number = 0;
  iWorkerId: number = 0;
  lBranches: CodeTable[] = [];
  iBranchId: number = 0;
  dtValidityDate: Date = new Date();
  dtLatestentering: Date = new Date();
  isConcentratedReport: number = 0;
  nvUserMail: string = "";
  nvUserPhone: string = "";
  iActivityPermissionId: number = 0;
}
