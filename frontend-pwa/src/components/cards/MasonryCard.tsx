import * as React from 'react';
import { styled } from '@mui/material/styles';
import Paper from '@mui/material/Paper';
import { Box, Typography, IconButton } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete'
import EditIcon from '@mui/icons-material/Edit'
import CloseIcon from '@mui/icons-material/Close'
import Close from '@mui/icons-material/Close';

const Item = styled(Paper)(({ theme }) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    ...theme.typography.body2,
    padding: theme.spacing(0.5),
    boxShadow: `1px 0px 3px 0px ${theme.palette.secondary.main}`
}));
interface MasonryCardProps {
    title: string
    id: string
    onDelete: ((id: string) => void) | undefined
    onEdit: ((id: string) => void) | undefined
    onClose: (() => void) | undefined
    children: React.ReactNode[] | React.ReactNode
}

const MasonryCard: React.FunctionComponent<MasonryCardProps> = ({ title, id, onDelete = undefined, onEdit = undefined, onClose = undefined, children }) => {
    return (
        <Item>
            <Box style={{ paddingLeft: ".9em" }}>
                <Box display="flex" justifyContent={"space-between"}>
                    <Box>
                        <Typography variant='h6'>{title}</Typography>
                    </Box>
                    <Box>
                        {
                            onEdit && <IconButton aria-label="edit" color="secondary" size="small" onClick={() => onEdit(id)}>
                                <EditIcon fontSize="inherit" />
                            </IconButton>
                        }
                        {
                            onDelete && <IconButton aria-label="delete" color="secondary" size="small" onClick={() => onDelete(id)}>
                                <DeleteIcon fontSize="inherit" />
                            </IconButton>
                        }

                        {
                            onClose && <IconButton aria-label="edit" color="secondary" size="small" onClick={onClose}>
                                <CloseIcon fontSize="inherit" />
                            </IconButton>
                        }
                    </Box>
                </Box>

                <hr />
                {children}
            </Box>
        </Item>
    );
}

export default MasonryCard;

