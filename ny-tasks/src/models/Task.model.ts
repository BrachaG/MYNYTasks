export interface Task {
    iTaskId: number;
    iTargetId: number;
    iType: number;
    nvCategory: string;
    dtEndDate: Date;
    iStatus: number;
    iStudentId: number|null;
    nvOrigin: string;
    nvComments: string;
    nvStatus:string;
    nvType:string;
}