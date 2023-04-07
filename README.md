# Travail Pratique 3
## Gestion de flux de données

### Context
Ce dossier comprends des fichiers de configuration kubernetes qui vous permetterons de:
* Lancer un server de Base de Données `MySQL`
* Lancer `kafka`
* Lancer un générateur de flux de données
* Lancer un consomateur de flux de données qui calcule votre performance

### Objectifs
* Construire un service qui consomme les usagers depuis le topic `users` en format json
** Pour chaque user:
*** Obtenir son adresse couriel depuis son nom
*** Ajouter le champ `email` à l'object json reçu
*** publier le nouvel objet dans le topic kafka `notification`
* Obtenir le plus court temps de traitement possible pour l'ensemble du fluxpowe
* Documenter toutes vos experimentations et leurs résultats par écrit

Les courriels des usasgers sont stocké dans la base de donné

### Limitation
Il est interdit de modifier les images utilisées pour le notification-consumer.
Des messages au format string non formatés en json peuvent subvenir. Vous devez les transmettre tel quel vers le topic `notification`

## Comment Deployer les serveurs ?
kubectl apply -f server/.  
kubectl delete -f server/.  

## Command lancer le flux de donner?
kubectl replace --force -f user-producer.yaml 

Rgarder les logs du pod `notification-consumer-deployment` pour voir votre résultat:
```
kubectl logs --follow notification-consumer-deployment-75bdfd596f-glqkd
Starting notification consumer
2021/04/09 20:01:27 DEBUT DE TRAITEMENT DU LOT
2021/04/09 20:01:27 Email sent to: stephane@brillant.ca
2021/04/09 20:01:27 Email sent to: stephane@brillant.ca
2021/04/09 20:01:27 Email sent to: stephane@brillant.ca
2021/04/09 20:01:29 TRAITEMENT DU LOT COMPLÉTÉ EN : 1.527583 secondes

```

## Comment écrire et lire dans les topic depuis la ligne de commande?
kubectl run -ti --image=gcr.io/google_containers/kubernetes-kafka:1.0-10.2.1 consume --restart=Never --rm -- kafka-console-consumer.sh --topic users --bootstrap-server kafka:9092
kubectl run -ti --image=gcr.io/google_containers/kubernetes-kafka:1.0-10.2.1 produce --restart=Never --rm -- kafka-console-producer.sh --topic notification --broker-list kafka-0.kafka.default.svc.cluster.local:9092,kafka-1.kafka.default.svc.cluster.local:9092,kafka-2.kafka.default.svc.cluster.local:9092 done;
