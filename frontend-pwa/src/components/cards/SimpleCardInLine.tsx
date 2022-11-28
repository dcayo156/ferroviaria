import * as React from 'react';
import { Box, IconButton, styled, useTheme } from '@mui/material'
import DeleteIcon from '@mui/icons-material/Delete'
import EditIcon from '@mui/icons-material/Edit'
interface SimpleCardInLineProps {
    id: string
    title: string
    description: string
    onDelete: ((id: string) => void) | undefined
    onEdit: ((id: string) => void) | undefined
}
const HoverBox = styled(Box)(({ theme }) => ({
    '&:hover': {
        background: theme.palette.primary.light,
    },
    paddingLeft: 2,
}));

const SimpleCardInLine: React.FunctionComponent<SimpleCardInLineProps> = ({ id, title, description, onDelete, onEdit }) => {
    const theme = useTheme();
    return (
        <HoverBox display="flex" justifyContent={"space-between"} sx={{ pl: 2 }}>
            <Box display="flex" flex={8}>
                {title}
                <div style={{ paddingLeft: ".5em", paddingTop: ".2em", fontSize: ".8em", color: theme.palette.primary.dark }}>
                    {description && description != "" && `(${description})`}
                </div>
            </Box>
            <Box flex={2}>
                {
                    onEdit && <IconButton aria-label="edit" size="small" onClick={() => onEdit(id)}>
                        <EditIcon fontSize="inherit" />
                    </IconButton>
                }
                {
                    onDelete && <IconButton aria-label="delete" size="small" onClick={() => onDelete(id)}>
                        <DeleteIcon fontSize="inherit" />
                    </IconButton>
                }
            </Box>
        </HoverBox>
    );
}

export default SimpleCardInLine;