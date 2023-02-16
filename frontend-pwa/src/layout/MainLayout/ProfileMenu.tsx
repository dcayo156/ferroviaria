import { Menu, MenuItem, Divider, Box } from '@mui/material';
import Avatar from '@mui/material/Avatar';
import PersonIcon from '@mui/icons-material/Person';
import ListItemIcon from '@mui/material/ListItemIcon';
import Logout from '@mui/icons-material/Logout';
import KeyIcon from '@mui/icons-material/Key';
import MailIcon from '@mui/icons-material/Mail';
import DatosIcon from '@mui/icons-material/PersonPin';
import { getCredentials, logout } from '../../store/slices/Auth/localStorage'
import * as React from 'react';
import { Navigate, useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { setCredentials } from '../../store/slices/Auth'
import { useAuth } from '../../hook/useAuth'
interface ProfileMenuProps {
    anchorEl: HTMLElement | null,
    setAnchorEl: React.Dispatch<React.SetStateAction<HTMLElement | null>>
}

const ProfileMenu: React.FunctionComponent<ProfileMenuProps> = ({ anchorEl, setAnchorEl }) => {
    const auth = useAuth()
    const {id}= getCredentials();
    const navigation=useNavigate();
    const dispatch=useDispatch();
    const open = Boolean(anchorEl);
    
    const handleClose = () => {
        setAnchorEl(null);
    };
    const onLogout = () => {
        logout();
        dispatch(setCredentials({
            id:null,
            username:null,
            email:null,
            token:null,
            refreshToken:null
        }));
        navigation("/");
    }
    const navigationTo= (url:string) => {
        navigation(url);
    }
    return <Menu
        anchorEl={anchorEl}
        id="account-menu"
        open={open}
        onClose={handleClose}
        onClick={handleClose}
        PaperProps={{
            elevation: 0,
            sx: {
                overflow: 'visible',
                filter: 'drop-shadow(0px 2px 8px rgba(0,0,0,0.32))',
                mt: 1.5,
                '& .MuiAvatar-root': {
                    width: 32,
                    height: 32,
                    ml: -0.5,
                    mr: 1,
                },
                '&:before': {
                    content: '""',
                    display: 'block',
                    position: 'absolute',
                    top: 0,
                    right: 14,
                    width: 10,
                    height: 10,
                    bgcolor: 'background.paper',
                    transform: 'translateY(-50%) rotate(45deg)',
                    zIndex: 0,
                },
            },
        }}
        transformOrigin={{ horizontal: 'right', vertical: 'top' }}
        anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
    >
        <div style={{display:"flex", flexDirection:"column", alignItems:"center", paddingBottom:"5px"}}>
            <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }} style={{width:"65px", height:"65px"}}>
                <PersonIcon style={{ fontSize: "65px" }}/>
            </Avatar>
            {auth.user}
        </div>
        <Divider />
        {/* <MenuItem sx={{p:1}}>
            <ListItemIcon>
                <MailIcon fontSize="small" />
            </ListItemIcon>
            Contacto
        </MenuItem> */}
        {/* <MenuItem sx={{p:1}} onClick={()=>navigationTo("/auth/show/user-authenticated")}>
            <ListItemIcon>
                <DatosIcon fontSize="small" />
            </ListItemIcon>
            Mis Datos
        </MenuItem> */}
        {/* <MenuItem sx={{p:1}} onClick={()=>navigationTo(`/auth/change-password/${id}`)}>
            <ListItemIcon>
                <KeyIcon fontSize="small" />
            </ListItemIcon>
            Cambiar contraseña
        </MenuItem> */}
        <MenuItem sx={{p:1}} onClick={onLogout}>
            <ListItemIcon>
                <Logout fontSize="small" />
            </ListItemIcon>
            Logout
        </MenuItem>
    </Menu>;
}

export default ProfileMenu;