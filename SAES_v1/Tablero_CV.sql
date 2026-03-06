
select campus, Round(Vencido/ Xcobrar*100,2) Porcentaje from (

select tprog_tnive_clave, tnive_desc nivel, sum(tedcu_importe) Xcobrar, 

(select sum(tedcu_balance) from tedcu xx, tcoco, testu e, tprog
 where x.tedcu_tpees_clave=xx.tedcu_tpees_clave and xx.tedcu_tcoco_clave=tcoco_clave
 and   tcoco_categ='CO' and tedcu_fecha_venc < curdate()
 and   testu_tpers_num=tedcu_tpers_num and testu_tpees_clave in 
(select max(testu_tpees_clave) from testu ee
 where e.testu_tpers_num=ee.testu_tpers_num)
 and   z.testu_tnive_clave=e.testu_tnive_clave
 and   e.testu_tcamp_clave='LV') Vencido

from tedcu x, tcoco, testu z, tcamp, tprog
where tedcu_tpees_clave='202065'
and   tedcu_tcoco_clave=tcoco_clave and tcoco_categ='CO'
and   testu_tpers_num=tedcu_tpers_num and testu_tpees_clave in 
(select max(testu_tpees_clave) from testu zz
 where z.testu_tpers_num=zz.testu_tpers_num)
and    testu_tprog_clave=tprog_clave and tprog_tnive_clave=tnive_clave
and    testu_tcamp_clave='LV'
group by testu_tnive_clave, tnive_desc

) x;
-------------------------------------------------------------------------------------
select nivel, Round(Vencido/ Xcobrar*100,2) Porcentaje from (
select tprog_tnive_clave, tnive_desc nivel, sum(tedcu_importe) Xcobrar, 
(select sum(tedcu_balance) from tedcu xx, tcoco, testu e, tprog
 where x.tedcu_tpees_clave=xx.tedcu_tpees_clave and xx.tedcu_tcoco_clave=tcoco_clave
 and   tcoco_categ='CO' and tedcu_fecha_venc < curdate()
 and   testu_tpers_num=tedcu_tpers_num and testu_tpees_clave in 
(select max(testu_tpees_clave) from testu ee
 where e.testu_tpers_num=ee.testu_tpers_num)
 and   testu_tprog_clave=tprog_clave
 and   e.testu_tcamp_clave='LV') Vencido
from tedcu x, tcoco, testu z, tnive, tprog
where tedcu_tpees_clave='202065'
and   tedcu_tcoco_clave=tcoco_clave and tcoco_categ='CO'
and   testu_tpers_num=tedcu_tpers_num and testu_tpees_clave in 
(select max(testu_tpees_clave) from testu zz
 where z.testu_tpers_num=zz.testu_tpers_num)
 and   testu_tprog_clave=tprog_clave
and    tprog_tnive_clave=tnive_clave and testu_tcamp_clave='LV'
group by tprog_tnive_clave, tnive_desc
) x;

-- SR LICENCIATURA	25.83
-- LV LICENCIATURA	54.11

select programa, Round(Vencido/ Xcobrar*100,2) Porcentaje from (
select tprog_clave, tprog_desc programa, sum(tedcu_importe) Xcobrar, 
(select sum(tedcu_balance) from tedcu xx, tcoco, testu e, tprog
 where x.tedcu_tpees_clave=xx.tedcu_tpees_clave and xx.tedcu_tcoco_clave=tcoco_clave
 and   tcoco_categ='CO' and tedcu_fecha_venc < curdate()
 and   testu_tpers_num=tedcu_tpers_num and testu_tpees_clave in 
(select max(testu_tpees_clave) from testu ee
 where e.testu_tpers_num=ee.testu_tpers_num)
 and   testu_tprog_clave=tprog_clave
 and   e.testu_tcamp_clave='LV' and tprog_tnive_clave='LI'
 and   z.testu_tprog_clave=e.testu_tprog_clave) Vencido
from tedcu x, tcoco, testu z,  tprog
where tedcu_tpees_clave='202065'
and   tedcu_tcoco_clave=tcoco_clave and tcoco_categ='CO'
and   testu_tpers_num=tedcu_tpers_num and testu_tpees_clave in 
(select max(testu_tpees_clave) from testu zz
 where z.testu_tpers_num=zz.testu_tpers_num)
 and   testu_tprog_clave=tprog_clave
and    testu_tcamp_clave='LV' and tprog_tnive_clave='LI'
group by tprog_clave, tprog_desc
) x;
