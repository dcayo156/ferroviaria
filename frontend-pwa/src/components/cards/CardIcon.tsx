import React from 'react';
import Button from '@mui/material/Button';
import { Card, CardActionArea, CardActions ,CardContent, CardHeader, Divider,CardMedia, Typography,Color } from '@mui/material';
import {IAccessProgram} from '../../store/types/AccessProgram'
import { URL_API_V1 } from "../../store/services";

interface ICardIcon {
  accessProgram:IAccessProgram
}

const CardIcon:React.FunctionComponent<ICardIcon>=({accessProgram})=> {
  return (
    <Card raised sx={{maxWidth: 345,background:"#FCA101"}}>
      <CardActionArea onClick={()=>{
        window.open(
          accessProgram.url,
          '_blank' 
        );
      }}>
        <CardMedia
          component="img"
          sx={{height:200, padding: "1em 1em 1em 1em", objectFit: "contain"}}
          image={`${URL_API_V1}Programs/FindProgramsFileById/${accessProgram.id}`}
          title={`${accessProgram.iconName}`}
        />
        <CardContent sx={{background:"#308200",color:"#fff"}}>
          <Typography gutterBottom variant="h5" component="h2" style={{textAlign:"center"}}>
            {accessProgram.name}
          </Typography>
        </CardContent>
      </CardActionArea>
    </Card>
  );
}

export default CardIcon;
