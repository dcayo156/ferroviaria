import * as React from 'react';
import SaveIcon from '@mui/icons-material/Save';
import EditIcon from '@mui/icons-material/Edit';
import { Box, TextField, Button, IconButton } from '@mui/material';
import { ITagCategory } from '../../../store/types/delete-Category'
import { useCreateTagCategoryMutation, useUpdateTagCategoryMutation } from '../../../store/services/Tag'
import { FetchBaseQueryError } from '@reduxjs/toolkit/dist/query';
import { SerializedError } from '@reduxjs/toolkit';
import { hasError } from '../../../components/Security/ErrorManager';
import { toast } from 'react-toastify';
import LoadingButtonIcon from '../../../components/Buttons/LoadingButtonIcon'
interface CreateTagCategoryProps {
    isEdit: boolean
    setIsEdit: React.Dispatch<React.SetStateAction<boolean>>
    category:Partial<ITagCategory>
    setCategory: React.Dispatch<React.SetStateAction<Partial<ITagCategory>>>
}

const CreateTagCategory: React.FunctionComponent<CreateTagCategoryProps> = ({ category,setCategory, isEdit, setIsEdit}) => {
    const [
        createTagCategory,
        { isLoading },
    ] = useCreateTagCategoryMutation();
    const [
        updateTagCategory,
        { isLoading:isUpdating },
    ] = useUpdateTagCategoryMutation();
    const onSaveCategory = () => {
        createTagCategory(category).then((response: { data: string } | { error: FetchBaseQueryError | SerializedError; }) => {
            if (hasError(response, "Error al momento crear categoria")) {
                return;
            }
            if ("data" in response) {
                toast.success(`Categoria: ${category.description} ha sido creado existosamente`);
                setCategory({ ...category, description: "" });
            }
        });
    }
    const onEditCategory = () => {
        updateTagCategory(category).then((response: { data: number } | { error: FetchBaseQueryError | SerializedError; }) => {
            if (hasError(response, "Error al momento crear categoria")) {
                return;
            }
            if ("data" in response) {
                toast.success(`Categoria: ${category.description} ha sido actualizado existosamente`);
                setIsEdit(false);
                setCategory({ ...category,id:"", description: "" });
            }
        });
    }
    return (
        <Box display={"flex"} >
            <Box flex="9">
                <TextField
                    size='small'
                    margin="normal"
                    required
                    fullWidth
                    id="description"
                    label="Descripcion"
                    name="description"
                    value={category.description}
                    onChange={((e) => setCategory({ ...category, description: e.target.value }))}
                    autoFocus
                />
            </Box>
            <Box flex="3">
                {
                    isEdit ?
                        <LoadingButtonIcon
                            loading={isUpdating}
                            text="Editar"
                            onClick={onEditCategory}
                            startIcon={<EditIcon fontSize="inherit" />} />
                        :
                        <LoadingButtonIcon
                            loading={isLoading}
                            text="Guardar"
                            onClick={onSaveCategory}
                            startIcon={<SaveIcon fontSize="inherit" />} />
                }

            </Box>

        </Box >
    );
}

export default CreateTagCategory;