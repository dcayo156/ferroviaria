import { ListItem, ListItemButton, ListItemIcon, ListItemText, useMediaQuery, useTheme } from '@mui/material';
import * as React from 'react';
import { useNavigate } from 'react-router-dom';
interface ItemToProps {
    to:string,
    text:string,
    open?:boolean,
    children: JSX.Element,
    active:boolean,
    switchOpen: () => void
}
 
const ItemTo: React.FunctionComponent<ItemToProps> = ({to,text,open,active,children,switchOpen}) => {
    const navigate = useNavigate();
    const theme=useTheme();
    const isMdUp = useMediaQuery(theme.breakpoints.up('md'));
    const handleto = (url:string) => {
        navigate(url);
        
        if (!isMdUp){
            switchOpen();
        }
    };
    return ( <ListItem key={text} disablePadding sx={{ display: 'block' }}>
    <ListItemButton onClick={()=>handleto(to)}
        sx={{
            minHeight: 20,
            minWidth:150,
            justifyContent: open ? 'initial' : 'center',
            px: 2.5,
            backgroundColor:active?theme.palette.primary.main:""
        }}
    >
        <ListItemIcon
            sx={{
                minWidth: 0,
                mr: open ? 3 : 'auto',
                justifyContent: 'center',
            }}
        >
            {children}
        </ListItemIcon>
        <ListItemText primary={text} sx={{ opacity: open ? 1 : 0 }} />
    </ListItemButton>
</ListItem> );
}
 
export default ItemTo;