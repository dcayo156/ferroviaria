import React from 'react';
import Button from '@mui/material/Button';
import { Card, CardActionArea, CardActions ,CardContent, CardHeader, Divider,CardMedia, Typography,Color } from '@mui/material';


export default function CardIcon() {
  return (
    <Card sx={{maxWidth: 345}}>
      <CardActionArea>
        <CardMedia
          sx={{height:140}}
          image="/static/images/cards/contemplative-reptile.jpg"
          title="Contemplative Reptile"
        />
        <CardContent>
          <Typography gutterBottom variant="h5" component="h2">
            Lizard
          </Typography>
          <Typography variant="body2" color="textSecondary" component="p">
            Lizards are a widespread group of squamate reptiles, with over 6,000 species, ranging
            across all continents except Antarctica
          </Typography>
        </CardContent>
      </CardActionArea>
      <CardActions>
        <Button size="small" color="primary">
          Share
        </Button>
        <Button size="small" color="primary">
          Learn More
        </Button>
      </CardActions>
    </Card>
  );
}
