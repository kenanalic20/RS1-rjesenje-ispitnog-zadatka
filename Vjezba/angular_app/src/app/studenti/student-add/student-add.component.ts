import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../moj-config";

@Component({
  selector: 'app-student-add',
  templateUrl: './student-add.component.html',
  styleUrls: ['./student-add.component.css']
})
export class StudentAddComponent implements OnInit {
  @Input() ime:any;
  @Input()prezime:any;
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
  dodajStudenta(){
    let body={
      ime:this.ime,
      prezime:this.prezime,
      opstinaId:this.odabranaOpstina||this.defaultOpstina
    }
    this.httpKlijent.post(MojConfig.adresa_servera+'/Student/AddStudent',body).subscribe(res=>{
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
