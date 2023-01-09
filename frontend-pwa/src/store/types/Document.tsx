export interface IDocument{
    id:string,
    fileName: string,
    filePath: string,
    photoName: string,
    photoPath: string,
    categoryId: string,
    subCategoryId: string
}


export interface IDocumentImage  {
    id:string,
    fileName: string,
    file: string,
    filePath: string,
    isFile: boolean
}

export interface IDocumentImageResponse  {
    filePath: string,
    fileName: string
}