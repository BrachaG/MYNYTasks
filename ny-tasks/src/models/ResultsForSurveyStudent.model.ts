import { Answer } from "./Answer.model"

export interface ResultsForSurveyStudent {
    iStudentId: number
    dtCreateDate: Date
    nvFullName: string
    nvGender: string
    nvBranchName: string
    nvMobileNumber: string
    nvEmail: string
    nvProgramName: string
    lAnswers: Answer[]
}