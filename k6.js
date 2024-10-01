import http from 'k6/http';
import { check, sleep } from 'k6';

// Configuração do teste
export let options = {
  stages: [
    { duration: '30s', target: 50 },  // Aumenta para 50 usuários em 30 segundos
    { duration: '1m', target: 100 },  // Mantém 100 usuários por 1 minuto
    { duration: '30s', target: 0 },   // Diminui para 0 usuários em 30 segundos
  ],
};

export default function () {
  let res = http.get('http://localhost:30000/api/products/category?request=Sobremesa');

  // Verifica se a resposta está com o status 200
  check(res, {
    'status was 200': (r) => r.status === 200,
  });

  // Pausa de 1 segundo entre as requisições
  sleep(1);
}