import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../moj-config";


@Component({
  selector: 'app-student-edit',
  templateUrl: './student-edit.component.html',
  styleUrls: ['./student-edit.component.css']
})
export class StudentEditComponent  {
  @Input() data:any;
  @Input() defaultOpstina:any;
  @Input() showModal:boolean;
  @Output() closeModal:any=new EventEmitter<void>();
  @Output() reload:any=new EventEmitter<void>();
  opstine:any;
  odabranaOpstina:any;
  constructor(private httpKlijent:HttpClient) { }
  reloadPage(){
    this.reload.emit();
  }
 zatvoriModal(){
   this.closeModal.emit()
 }
 fetchOpstina(){
    this.httpKlijent.get(MojConfig.adresa_servera+"/Opstina/GetByAll").subscribe(res=>{
      this.opstine=res;
      console.log(this.opstine)
    })
 }
 editujStudenta(){
    let body={
      id:this.data.id,
      ime:this.data.ime,
      prezime:this.data.prezime,
      opstinaId:this.odabranaOpstina||this.defaultOpstina
    }
    this.httpKlijent.put(MojConfig.adresa_servera+'/Student/EditStudent',body).subscribe(res=>{
      this.data=res;
      this.reloadPage();
    })
 }
  ngOnInit(): void {
    this.fetchOpstina();

  }

  odaberiOpstinu(event:any) {
    this.odabranaOpstina=event.target.value;
    console.log(this.odabranaOpstina)
  }
}
