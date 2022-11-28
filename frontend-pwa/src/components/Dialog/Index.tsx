import * as React from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";

type Props = {
  title: string;
  openDialog:boolean;
  children: JSX.Element;
  okAction : ()=>void;
  cancelAction : ()=>void;
};

export default function DialogForm(props: Props) {
  const handleOKAction = () => {
    props.okAction();
  };

  const handleCancelAction = () => {
    props.cancelAction();
  };

  return (
    <div>
      <Dialog open={props.openDialog}>
        <DialogTitle>{props.title}</DialogTitle>
        <DialogContent>{props.children}</DialogContent>
        <DialogActions>
          <Button onClick={handleCancelAction}>Cancelar</Button>
          <Button onClick={handleOKAction}>Aceptar</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
