import * as React from 'react';
import Box from '@mui/material/Box';
import { styled } from '@mui/material/styles';
import { Button, ButtonProps } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import Masonry from '@mui/lab/Masonry';
import { ITagCategory } from '../../../store/types/Category'
import { ITag } from '../../../store/types/delete-Tag'
import CreateTagCategory from '../Form/Create'
import MasonryCard from '../../../components/cards/MasonryCard'
import DialogCreateTag from '../../Tag/Forms/DialogCreate'
import DialogEditTag from '../../Tag/Forms/DialogEdit'
import { useDeleteTagCategoryMutation } from '../../../store/services/Tag'
import { FetchBaseQueryError } from '@reduxjs/toolkit/dist/query';
import { SerializedError } from '@reduxjs/toolkit';
import { hasError } from '../../../components/Security/ErrorManager';
import { toast } from 'react-toastify';
import FormTag from '../Form/FormTag'
interface TableProps {
    categories: ITagCategory[]
}
const ColorButton = styled(Button)<ButtonProps>(({ theme }) => ({
    color: theme.palette.secondary.main,
    textTransform: "capitalize",
    fontWeight: "bold",
    paddingLeft: "5px",
    margin: 1,
    '&:hover': {
        color: "#fff",
        backgroundColor: theme.palette.secondary.main,
    },
}));

const Table: React.FunctionComponent<TableProps> = ({ categories }) => {
    const [category, setCategory] = React.useState<Partial<ITagCategory>>({id:"", description:""})
    const [tag,setTag]=React.useState<ITag>({id:"",name:"",description:"",tagCategoryId:""});
    const [openDialog, setOpenDialog] = React.useState(false);
    const [openDialogEdit, setOpenDialogEdit] = React.useState(false);
    const [categoryId, setCategoryId] = React.useState("");
    const [editCategoryState, setEditCategoryState] = React.useState(false);

    const [
        deleteCategory,
        { isLoading: isDeleting },
    ] = useDeleteTagCategoryMutation()
    const onDeleteCategory = (id: string) => {
        var bool=window.confirm("Seguro de eliminar esta Categoria?")
        if(!bool)return;
        setCategory(categories.find(cat=>cat.id===id) as Partial<ITagCategory>);
        deleteCategory(id).then((response: { data: number; } | { error: FetchBaseQueryError | SerializedError; })=>{
            if (hasError(response, "Error al momento de eliminar categoria")) {
                return;
            }
            if ("data" in response) {
                toast.success(`Categoria: ${category.description} ha sido eliminado existosamente`);
                setCategory({ ...category, description: "" });
            }
        });
     }
    const onEditCategory = (id: string) => {
        setCategory(categories.find(cat=>cat.id===id) as Partial<ITagCategory>);
        setEditCategoryState(true);
    }
    const onCloseCategory = () => {
        setEditCategoryState(false);
        setCategory({id:"", description:""})
    }
    const onCreteTag = (id: string) => {
        setCategoryId(id);
        setOpenDialog(true);
    }
    const changeTag = (t:ITag) => {
        setTag(t);
        setOpenDialogEdit(true);
    }
    const reciveResponse = (value: boolean, response: any) => {
        
    }
    return (<Box sx={{ minHeight: 393 }}>
        <Masonry columns={3} spacing={2} >
            <MasonryCard
                key={"createtagcategory"}
                title={editCategoryState ? "Editar Categoria" : "Crear Nueva Categoria"}
                id="no pasa nada"
                onClose={editCategoryState ? onCloseCategory : undefined}
                onDelete={undefined}
                onEdit={undefined}
            >
                <CreateTagCategory
                    category={category}
                    setCategory={setCategory}
                    isEdit={editCategoryState}
                    setIsEdit={setEditCategoryState}
                />
            </MasonryCard>
            {categories.map((category, index) => (
                <MasonryCard
                    key={index}
                    title={category.description}
                    id="no pasa nada"
                    onClose={undefined}
                    onDelete={()=>onDeleteCategory(category.id)}
                    onEdit={()=>onEditCategory(category.id)}
                >
                    {
                        category.tags.map(tag => {
                            return <FormTag key={tag.id} tag={tag} changeTag={changeTag} />
                        })
                    }
                    <Box key={"tag.id"} sx={{ pl: 2 }}>
                        <ColorButton onClick={() => onCreteTag(category.id)} size="small" endIcon={<AddIcon fontSize="inherit" />}>
                            Agregar Tag
                        </ColorButton>
                    </Box>
                </MasonryCard>
            ))}
        </Masonry>
        {
            openDialog && <DialogCreateTag name="" categoryId={categoryId} openDialog={openDialog} setOpenDialog={setOpenDialog} sendResponse={reciveResponse} key="createTag" />
        }
        {
            openDialogEdit && <DialogEditTag tag={tag} setTag={setTag} categories={categories} openDialog={openDialogEdit} setOpenDialog={setOpenDialogEdit} sendResponse={reciveResponse} key="EditTag" />
        }
    </Box>);
}

export default Table;
