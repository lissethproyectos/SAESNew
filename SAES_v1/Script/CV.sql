 select Campus, Nivel, format(Xcobrar_Par,2) Xcobrar_Par, fecha(date_format(FV1,'%Y-%m-%d')) FV1, format(Vencido1,2) Vencido1, round(Vencido1/ Xcobrar_Par*100,2) Porc1,
 fecha(date_format(FV2,'%Y-%m-%d')) FV1, format(Vencido2,2) Vencido2, round(Vencido2/ Xcobrar_Par*100,2) Porc2,
 fecha(date_format(FV3,'%Y-%m-%d')) FV1, format(Vencido3,2) Vencido3, round(Vencido3/ Xcobrar_Par*100,2) Porc3,
 fecha(date_format(FV4,'%Y-%m-%d')) FV1, format(Vencido4,2) Vencido4, round(Vencido4/ Xcobrar_Par*100,2) Porc4,
 format(Xcobrar_Par*4,2) Xcobrar_Total, format(Vencido1+Vencido2+Vencido3+Vencido4,2) Total_Vencido,
 round((Vencido1+Vencido2+Vencido3+Vencido4)/(Xcobrar_Par*4)*100,2) Porc_Vencido_Total
 from (
select tcamp_desc Campus, tnive_desc Nivel, sum(tedcu_importe) Xcobrar_Par, 

(select tfeve_vencimiento from tfeve where tfeve_tpees_clave=x.tedcu_tpees_clave 
  and   tfeve_ttasa_clave='01' and tfeve_numero=1) FV1,
 (select sum(tedcu_balance) from tedcu xx, tcoco, testu e, tprog
 where x.tedcu_tpees_clave=xx.tedcu_tpees_clave and xx.tedcu_tcoco_clave=tcoco_clave
 and   tcoco_categ='CO' and tedcu_fecha_venc < curdate() and xx.tedcu_numero=1
 and   testu_tpers_num=tedcu_tpers_num and testu_tpees_clave in 
(select max(testu_tpees_clave) from testu ee  where e.testu_tpers_num=ee.testu_tpers_num)
 and   testu_tprog_clave=tprog_clave and   e.testu_tcamp_clave=z.testu_tcamp_clave) Vencido1,
 
 (select tfeve_vencimiento from tfeve where tfeve_tpees_clave=x.tedcu_tpees_clave 
  and   tfeve_ttasa_clave='01' and tfeve_numero=2) FV2,
 (select sum(tedcu_balance) from tedcu xx, tcoco, testu e, tprog
 where x.tedcu_tpees_clave=xx.tedcu_tpees_clave and xx.tedcu_tcoco_clave=tcoco_clave
 and   tcoco_categ='CO' and tedcu_fecha_venc < curdate() and xx.tedcu_numero=2
 and   testu_tpers_num=tedcu_tpers_num and testu_tpees_clave in 
(select max(testu_tpees_clave) from testu ee  where e.testu_tpers_num=ee.testu_tpers_num)
 and   testu_tprog_clave=tprog_clave and   e.testu_tcamp_clave=z.testu_tcamp_clave) Vencido2,
 
 (select tfeve_vencimiento from tfeve where tfeve_tpees_clave=x.tedcu_tpees_clave 
  and   tfeve_ttasa_clave='01' and tfeve_numero=3) FV3,
 (select sum(tedcu_balance) from tedcu xx, tcoco, testu e, tprog
 where x.tedcu_tpees_clave=xx.tedcu_tpees_clave and xx.tedcu_tcoco_clave=tcoco_clave
 and   tcoco_categ='CO' and tedcu_fecha_venc < curdate() and xx.tedcu_numero=3
 and   testu_tpers_num=tedcu_tpers_num and testu_tpees_clave in 
(select max(testu_tpees_clave) from testu ee  where e.testu_tpers_num=ee.testu_tpers_num)
 and   testu_tprog_clave=tprog_clave and  e.testu_tcamp_clave=z.testu_tcamp_clave) Vencido3,

(select tfeve_vencimiento from tfeve where tfeve_tpees_clave=x.tedcu_tpees_clave 
  and   tfeve_ttasa_clave='01' and tfeve_numero=4) FV4,
 (select sum(tedcu_balance) from tedcu xx, tcoco, testu e, tprog
 where x.tedcu_tpees_clave=xx.tedcu_tpees_clave and xx.tedcu_tcoco_clave=tcoco_clave
 and   tcoco_categ='CO' and tedcu_fecha_venc < curdate() and xx.tedcu_numero=4
 and   testu_tpers_num=tedcu_tpers_num and testu_tpees_clave in 
(select max(testu_tpees_clave) from testu ee  where e.testu_tpers_num=ee.testu_tpers_num)
 and   testu_tprog_clave=tprog_clave and  e.testu_tcamp_clave=z.testu_tcamp_clave) Vencido4

from tedcu x, tcoco, testu z, tnive, tprog, tcamp
where tedcu_tpees_clave='202065' and   tedcu_tcoco_clave=tcoco_clave and tcoco_categ='CO'
and   testu_tpers_num=tedcu_tpers_num and testu_tpees_clave in 
(select max(testu_tpees_clave) from testu zz
 where z.testu_tpers_num=zz.testu_tpers_num)
 and   testu_tprog_clave=tprog_clave
and    tprog_tnive_clave=tnive_clave 
and   tedcu_numero=1 and testu_tcamp_clave=tcamp_clave
group by Campus, Nivel) x
order by Campus, Nivel;


