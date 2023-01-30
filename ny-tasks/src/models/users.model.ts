import { CodeTable } from "./CodeTable.model";

export interface user {
  iUserId: number ;
  nvUserName: string ;
  nvPassword: string ;
  iUserStatus: number;
  iOrganizationId: number;
  iPermissionLevelId: number ;
  iReportedHoursTypeId: number ;
  iContinueContactPermissionId: number ;
  iWorkerId: number ;
  lBranches: CodeTable;
  iBranchId: number ;
  dtValidityDate: Date ;
  dtLatestentering: Date ;
  isConcentratedReport: number ;
  nvUserMail: string ;
  nvUserPhone: string;
  iActivityPermissionId: number ;
}
