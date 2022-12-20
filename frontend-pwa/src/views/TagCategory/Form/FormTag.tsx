import * as React from 'react';
import { ITag } from '../../../store/types/delete-Tag'
import SimpleCardInLine from '../../../components/cards/SimpleCardInLine';
import { useUpdateTagMutation,useDeleteTagMutation } from '../../../store/services/Tag'
import { FetchBaseQueryError } from '@reduxjs/toolkit/dist/query';
import { SerializedError } from '@reduxjs/toolkit';
import { hasError } from '../../../components/Security/ErrorManager';
import { toast } from 'react-toastify';
interface FormTagProps {
    tag:ITag
    changeTag: (t:ITag) => void
}
 
const FormTag: React.FunctionComponent<FormTagProps> = ({tag,changeTag}) => {
    const [
        deleteTag,
        { isLoading:isUpdating },
    ] = useDeleteTagMutation();
    const onDelete=(id:string)=>{
        var bool=window.confirm("Seguro de eliminar esta Categoria?")
        if(!bool)return;
        deleteTag(id).then((response: { data: number } | { error: FetchBaseQueryError | SerializedError; }) => {
            if (hasError(response, "Error al momento crear categoria")) {
                return;
            }
            if ("data" in response) {
                toast.success(`Tag: ${tag.name} ha sido eliminado existosamente`);
            }
        });
    }
    const onEdit = (id:string) => {
        changeTag(tag);
    }
    return ( <SimpleCardInLine 
        key={tag.id} 
        id={tag.id} 
        title={tag.name} 
        description={tag.description}
        onDelete={onDelete} 
        onEdit={onEdit} /> );
}
 
export default FormTag;