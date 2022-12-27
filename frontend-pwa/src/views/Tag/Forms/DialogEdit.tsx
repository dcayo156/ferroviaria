import * as React from 'react';
import DialogForm from "../../../components/Dialog/Index";
import { Box, TextField } from "@mui/material";
import { ITag } from '../../../store/types/delete-Tag';
import { ITagCategory } from '../../../store/types/delete-Category';
import { useUpdateTagMutation } from '../../../store/services/Tag';
import { toast } from 'react-toastify';
import { FetchBaseQueryError } from '@reduxjs/toolkit/dist/query';
import { SerializedError } from '@reduxjs/toolkit';
import { hasError } from '../../../components/Security/ErrorManager';
interface DialogEditProps {
    tag: ITag
    setTag: React.Dispatch<React.SetStateAction<ITag>>
    categories: ITagCategory[] | undefined
    openDialog: boolean
    setOpenDialog: (value: React.SetStateAction<boolean>) => void
    sendResponse: (value:boolean,response:any) => void
}

const DialogEdit: React.FunctionComponent<DialogEditProps> = ({ tag,setTag,categories, openDialog,setOpenDialog,sendResponse}) => {
    //const [tag, setTag] = React.useState<ITag>({ description: '', id: '', name: name, tagCategoryId: categoryId });
    const [updateTag, { isLoading: isUpdating }] = useUpdateTagMutation();
    
    const handleCreateTag=()=>{
        updateTag(tag).then((response: { data: number } | { error: FetchBaseQueryError | SerializedError; }) => {
            if(hasError(response,"Error al momento de crear el Tag")){
                sendResponse(false,response);
                return;
            }
            if ("data" in response) {
                toast.success(`Tag editado exitosamente`)
                sendResponse(true,response);
            }
            setOpenDialog(false);
        })
    }
    const handleClose=()=>{
        setOpenDialog(false);
    }
    return (<DialogForm
        openDialog={openDialog}
        title="Agregar Categoria"
        okAction={handleCreateTag}
        cancelAction={handleClose}
    >
        <div>
            <Box sx={{ mt: 2 }}>
                <TextField
                    select
                    label="Category"
                    sx={{ width: "100%", mt: 2 }}
                    value={tag.tagCategoryId}
                    onChange={({ target: { value } }) => setTag({ ...tag, "tagCategoryId": value })}
                    SelectProps={{
                        native: true,
                    }}
                >
                    {
                        categories && categories.map(categori => {
                            return <option key={categori.id} value={categori.id}>
                                {categori.description}
                            </option>
                        })
                    }
                </TextField>
            </Box>
            <Box sx={{ mt: 2 }}>
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    id="name"
                    label="Name"
                    name="name"
                    value={tag!.name}
                    onChange={({ target: { value } }) => setTag({ ...tag, "name": value })}
                    autoFocus
                />
            </Box>
            <Box sx={{ mt: 2 }}>
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    id="description"
                    label="Description"
                    name="description"
                    value={tag!.description}
                    onChange={({ target: { value } }) => setTag({ ...tag, "description": value })}
                    autoFocus
                />
            </Box>
        </div>
    </DialogForm>);
}

export default DialogEdit;
