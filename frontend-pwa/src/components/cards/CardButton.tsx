
import { useTheme } from '@mui/material/styles';
import { ButtonBase, IconButton, Link, Tooltip } from '@mui/material';
import PlusOneIcon from '@mui/icons-material/AddOutlined';
import ArrowBack from '@mui/icons-material/ArrowBack';
import { useNavigate } from 'react-router-dom';
// project imports


interface ICardButton {
    title:string
    link: string
    type: "plus" | "back"
}
const types={
    "plus":<PlusOneIcon  sx={{ width: 32, height: 32 }}/>,
    "back":<ArrowBack  sx={{ width: 32, height: 32 }}/>,
}

const CardButton: React.FunctionComponent<ICardButton> = ({ title, link, type}) => {
    const navigate = useNavigate();
    const redirectTo= (to:string)=>{
        navigate(to);
    }
    return (
        <Tooltip title={title || 'Reference'} placement="left">
            <IconButton
                size="small"
                edge="end"
                onClick={()=>redirectTo(link)}
                sx={{ ml: 2, bgcolor: 'primary.main' }}
            >
             {types[type]}   
            </IconButton>
        </Tooltip>
    );
};

export default CardButton;
