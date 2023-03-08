export interface IInspectionTrain{
    id:string,    
    codigo: string,
    fileName: string,
    observacionEvaluador: string,
    categoryId: string,
    subCategoryId: string,
    filePath: string,
}
export interface IInspectionTrainCreate{ 
    id: string,
    codigo: string,
    fileName: string,
    categoryId: string,
    subCategoryId: string,
    filePath: string,
}


export interface IInspectionTrainImage  {
    id:string,
    fileName: string,
    file: string,
    filePath: string,
    isFile: boolean
}

export interface IInspectionTrainImageResponse  {
    filePath: string,
    fileName: string
}


export interface IInspectionTrainOptions  {
    categoryId: string,
    subCategoryId: string
}
export interface IInspectionTrainPieChartFullVm  {
    labels: string[],
    datasets: Datasets
}
export interface Datasets  {
    label: string,
    data:number[],
    backgroundColor: string[],
    borderColor: string[],
    borderWidth: number
}