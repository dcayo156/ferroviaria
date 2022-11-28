import * as React from 'react';
import { Button, Link, Box } from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { useNavigate } from 'react-router-dom';
interface ActionButtonsProps {
    id:string
    editLink:string
    
}
 
const ActionButtons: React.FunctionComponent<ActionButtonsProps> = ({id,editLink}) => {
    const navigate = useNavigate();
    
    const onEdit = () => {
        navigate(editLink);
    }
    const onDelete = (e: any) => {
        
    }

    return <Box >
        <Link onClick={onEdit}><EditIcon /></Link>
        <Link onClick={onDelete}><DeleteIcon /></Link>
    </Box>
}
 
export default ActionButtons;