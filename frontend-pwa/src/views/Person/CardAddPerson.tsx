import { Avatar, Card, CardActions, CardContent, CardHeader, CardMedia, IconButton, Typography } from '@mui/material';
import * as React from 'react';
import { red } from '@mui/material/colors';
import PlusOne from '@mui/icons-material/PlusOne';
import { useNavigate } from 'react-router-dom'
import add_person_logo from '../../assets/img/add_person_logo.jpg'
interface CardAddPersonProps {
    
}
 
const CardAddPerson: React.FunctionComponent<CardAddPersonProps> = () => {
    const navigate = useNavigate();
    const addPerson= ()=>{
        navigate('/person/create');
    }
    return ( <Card sx={{ maxWidth: 345, marginBottom:"1em"}} >

        <CardHeader
            avatar={
                <Avatar sx={{ bgcolor: red[500] }} aria-label="recipe">
                    C
                </Avatar>
            }
            title="Crear una Person"
            subheader="Solo has click abajo"
        />

        <CardMedia
            component="img"
            height="194"
            image={add_person_logo}
            alt="image"
            sx={{cursor:"pointer"}}
            onClick={()=>addPerson()}
        />
        <CardContent>
            <Typography variant="body2" color="text.secondary">
                Crea mas personas con un solo click
            </Typography>
        </CardContent>
        <CardActions disableSpacing>
            <IconButton aria-label="add to favorites" onClick={()=>addPerson()}>
                <PlusOne /> Agregar una Persona
            </IconButton>
        </CardActions>

    </Card> );
}
 
export default CardAddPerson;