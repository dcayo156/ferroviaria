
export interface ITag{
    id:string
    name:string
    description:string
    tagCategoryId?:string|null
}
export interface selectValuesProps {
    value: string
    label: string
    labelCategory : string
    valueCategory: string
}
