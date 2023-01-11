
export interface ICategory {
    id: string
    name: string|undefined
    parentCategoryId:string|undefined
}
export interface ICategoryWithParent {
    id: string
    name: string|undefined
    parentCategoryId:string|undefined
    parentCategory:ICategory|null
}
export interface ICategoryWithChildren {
    id: string
    name: string|undefined
    parentCategoryId:string|undefined
    parentCategory:ICategory|null
    categories:ICategoryWithChildren[]|null
}