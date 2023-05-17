import { CodeTable } from "./CodeTable.model";

export interface userModel {
  iUserId: number;
  nvUserName: string;
  iPermissionLevelId: number;
  iContinueContactPermissionId: number;
  lBranches: CodeTable[] ;
  iBranchId: number ;
  iActivityPermissionId: number;
  token:string;
}
