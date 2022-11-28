import React, { useState } from "react";
import {
  Avatar,
  Divider,
  List,
  ListItem,
  ListItemAvatar,
  ListItemText,
  Typography,
} from "@mui/material";
import { IAddressPerson } from "../../store/types/Address";

interface ListAddressProps{
  people:IAddressPerson[]
  getSelectionPerson:(p:any)=>void
}
export const ListAddress: React.FunctionComponent<ListAddressProps> = ({getSelectionPerson,people}) => {
  const [idPerson, setIdPerson] = useState(null);

  const  showMarkOnMap = (Id: any) =>{
    var personAddress = people.find(Person => Person.personId===Id);
    setIdPerson(Id);
    getSelectionPerson(personAddress);
  }
  return (
    <List
      sx={{
        width: "100%",
        height: "65vh",
        overflowY: "scroll",
        boxShadow: "-5px -5px 3px #48529944",
      }}
    >
      {people.map((Person) => {
        return (
          <div key={Person.personId} onClick={() => showMarkOnMap(Person.personId)} >
            <ListItem alignItems="flex-start" selected={idPerson===Person.personId} 
                      sx={{cursor: 'pointer'}}>                
              <ListItemAvatar>
                <Avatar alt="Remy Sharp" src="/static/images/avatar/1.jpg" />
              </ListItemAvatar>
              <ListItemText
                primary={`${Person.firstName} ${Person.lastName}`}
                secondary={
                  <React.Fragment>
                    <Typography
                      sx={{ display: "inline" }}
                      component="span"
                      variant="body2"
                      color="text.primary"
                    >
                      {Person.description}
                    </Typography>
                  </React.Fragment>
                }
              />
            </ListItem>
            <Divider variant="inset" component="li" />
          </div>
        );
      })}
    </List>
  );
};
