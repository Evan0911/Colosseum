using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleState : State
{
    public BattleState(BattleSystem _system) : base(_system)
    {

    }

    //Fonction qui va regarder qui a fait quoi et en déduire ce qu'il se passe
    public override IEnumerator TurnBegin()
    {
        Action playerAction = system.playerAction;
        Action enemyAction = system.enemyAction;
        CreatureHealth playerHealth = system.playerHealth;
        CreatureHealth enemyHealth = system.enemyHealth;
        bool allyTypeAdvantage = system.allyTypeAdvantage;
        bool enemyTypeAdvantage = system.enemyTypeAdvantage;
        Text dialogue = system.dialogue;

        if (playerAction == Action.TARGETATTACK)
        {
            if (enemyAction == Action.TARGETATTACK)
            {
                //Les 2 se prennent les dégâts de leur attaque
                int damage = playerHealth.stats.atk - (enemyHealth.stats.def / 2);
                //On test l'avantage type
                if (allyTypeAdvantage)
                {
                    damage = damage * 3 / 2;
                }
                else if (enemyTypeAdvantage)
                {
                    damage = damage / 3 * 2;
                }

                //Pour éviter de heal l'ennemi
                if (damage < 0)
                {
                    damage = 0;
                }

                if (!enemyHealth.TakeDamage(damage))
                {
                    dialogue.text = "The enemy took " + damage + " damage.";
                    yield return new WaitForSeconds(2);
                }

                else
                {
                    dialogue.text = "The enemy took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    system.SetState(new WinState(system));
                    yield break;
                }

                damage = enemyHealth.stats.atk - (playerHealth.stats.def / 2);
                //On test l'avantage type
                if (enemyTypeAdvantage)
                {
                    damage = damage * 3 / 2;
                }
                else if (allyTypeAdvantage)
                {
                    damage = damage / 3 * 2;
                }

                //Pour éviter de heal l'ennemi
                if (damage < 0)
                {
                    damage = 0;
                }

                if (!playerHealth.TakeDamage(damage))
                {
                    dialogue.text = "You took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    NextTurn();
                    yield break;
                }

                else
                {
                    dialogue.text = "You took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    system.SetState(new LoseState(system));
                    yield break;
                }
            }

            else if (enemyAction == Action.AOEATTACK)
            {
                //L'ennemi se fait contrer son attaque, cependant les dégâts qu'il subit sont légèrement réduits
                int damage = (playerHealth.stats.atk - (enemyHealth.stats.def / 2)) / 2;
                //On test l'avantage type
                if (allyTypeAdvantage)
                {
                    damage = damage * 3 / 2;
                }
                else if (enemyTypeAdvantage)
                {
                    damage = damage / 3 * 2;
                }

                //Pour éviter de heal l'ennemi
                if (damage < 0)
                {
                    damage = 0;
                }

                if (!enemyHealth.TakeDamage(damage))
                {
                    dialogue.text = "The attack is weakened by the enemy's one.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "The enemy took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    NextTurn();
                }

                else
                {
                    dialogue.text = "The attack is weakened by the enemy's one.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "The enemy took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    system.SetState(new WinState(system));
                    yield break;
                }
            }

            else if (enemyAction == Action.BLOCK)
            {
                //L'ennemi bloque une partie des dégâts
                int damage = (playerHealth.stats.atk - (enemyHealth.stats.def / 2)) / 3;
                //On test l'avantage type
                if (allyTypeAdvantage)
                {
                    damage = damage * 3 / 2;
                }
                else if (enemyTypeAdvantage)
                {
                    damage = damage / 3 * 2;
                }

                //Pour éviter de heal l'ennemi
                if (damage < 0)
                {
                    damage = 0;
                }

                if (!enemyHealth.TakeDamage(damage))
                {
                    dialogue.text = "The enemy is blocking.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "The enemy took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    NextTurn();
                }

                else
                {
                    dialogue.text = "The enemy is blocking.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "The enemy took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    system.SetState(new WinState(system));
                    yield break;
                }
            }

            else if (enemyAction == Action.DODGE)
            {
                //L'ennemi esquive et contre attaque
                int damage = enemyHealth.stats.atk - (playerHealth.stats.def / 2);
                //On test l'avantage type
                if (enemyTypeAdvantage)
                {
                    damage = damage * 3 / 2;
                }
                else if (allyTypeAdvantage)
                {
                    damage = damage / 3 * 2;
                }

                //Pour éviter de heal l'ennemi
                if (damage < 0)
                {
                    damage = 0;
                }

                if (!playerHealth.TakeDamage(damage))
                {
                    dialogue.text = "The enemy counter your attack.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "You took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    NextTurn();
                }

                else
                {
                    dialogue.text = "The enemy counter your attack.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "You took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    system.SetState(new LoseState(system));
                    yield break;
                }
            }

            else if (enemyAction == Action.HEAL)
            {
                //Inflige des dégâts
                int damage = playerHealth.stats.atk - (enemyHealth.stats.def / 2);
                //On test l'avantage type
                if (allyTypeAdvantage)
                {
                    damage = damage * 3 / 2;
                }
                else if (enemyTypeAdvantage)
                {
                    damage = damage / 3 * 2;
                }

                //Pour éviter de heal l'ennemi
                if (damage < 0)
                {
                    damage = 0;
                }

                if (!enemyHealth.TakeDamage(damage))
                {
                    dialogue.text = "The enemy tried to heal.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "The enemy took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    NextTurn();
                }

                else
                {
                    dialogue.text = "The enemy tried to heal.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "The enemy took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    system.SetState(new WinState(system));
                    yield break;
                }
            }
        }

        else if (playerAction == Action.AOEATTACK)
        {
            if (enemyAction == Action.TARGETATTACK)
            {
                //Le joueur se prends des dégâts légèrement réduits
                int damage = (enemyHealth.stats.atk - (playerHealth.stats.def / 2)) / 2;
                //On test l'avantage type
                if (enemyTypeAdvantage)
                {
                    damage = damage * 3 / 2;
                }
                else if (allyTypeAdvantage)
                {
                    damage = damage / 3 * 2;
                }

                //Pour éviter de heal l'ennemi
                if (damage < 0)
                {
                    damage = 0;
                }

                if (!playerHealth.TakeDamage(damage))
                {
                    dialogue.text = "The enemy's attack is weakened by yours.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "You took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    NextTurn();
                }

                else
                {
                    dialogue.text = "The enemy's attack is weakened by yours.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "You took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    system.SetState(new LoseState(system));
                    yield break;
                }
            }

            else if (enemyAction == Action.AOEATTACK)
            {
                //Les attaques s'annulent
                dialogue.text = "Attacks cancelled together.";
                yield return new WaitForSeconds(2);

                NextTurn();
            }

            else if (enemyAction == Action.BLOCK)
            {
                //L'ennemi ne prends pas de dégâts (ou des dégâts très réduit)
                dialogue.text = "The enemy has blocked the attack";
                yield return new WaitForSeconds(2);

                NextTurn();
            }

            else if (enemyAction == Action.DODGE)
            {
                //L'ennmi se prends l'attaque de plein fouet
                int damage = (playerHealth.stats.atk * 3 / 4) - (enemyHealth.stats.def / 2);
                //On test l'avantage type
                if (allyTypeAdvantage)
                {
                    damage = damage * 3 / 2;
                }
                else if (enemyTypeAdvantage)
                {
                    damage = damage / 3 * 2;
                }

                //Pour éviter de heal l'ennemi
                if (damage < 0)
                {
                    damage = 0;
                }

                if (!enemyHealth.TakeDamage(damage))
                {
                    dialogue.text = "You hit the enemy in his dodge.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "The enemy took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    NextTurn();
                }

                else
                {
                    dialogue.text = "You hit the enemy in his dodge.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "The enemy took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    system.SetState(new WinState(system));
                    yield break;
                }
            }

            else if (enemyAction == Action.HEAL)
            {
                //L'ennmi se prends l'attaque de plein fouet
                int damage = (playerHealth.stats.atk * 3 / 4) - (enemyHealth.stats.def / 2);
                //On test l'avantage type
                if (allyTypeAdvantage)
                {
                    damage = damage * 3 / 2;
                }
                else if (enemyTypeAdvantage)
                {
                    damage = damage / 3 * 2;
                }

                //Pour éviter de heal l'ennemi
                if (damage < 0)
                {
                    damage = 0;
                }

                if (!enemyHealth.TakeDamage(damage))
                {
                    dialogue.text = "The enemy tried to heal";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "The enemy took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    NextTurn();
                }

                else
                {
                    dialogue.text = "The enemy tried to heal";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "The enemy took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    system.SetState(new WinState(system));
                    yield break;
                }
            }
        }

        else if (playerAction == Action.BLOCK)
        {
            if (enemyAction == Action.TARGETATTACK)
            {
                //Le joueur prends 3 fois moins de dégâts
                int damage = (enemyHealth.stats.atk - (playerHealth.stats.def / 2)) / 3;
                //On test l'avantage type
                if (enemyTypeAdvantage)
                {
                    damage = damage * 3 / 2;
                }
                else if (allyTypeAdvantage)
                {
                    damage = damage / 3 * 2;
                }

                //Pour éviter de heal l'ennemi
                if (damage < 0)
                {
                    damage = 0;
                }

                if (!playerHealth.TakeDamage(damage))
                {
                    dialogue.text = "You blocked the attack.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "You took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    NextTurn();
                }

                else
                {
                    dialogue.text = "You blocked the attack.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "You took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    system.SetState(new LoseState(system));
                    yield break;
                }
            }

            else if (enemyAction == Action.AOEATTACK)
            {
                //Le joueur ne prends aucun dégâts (ou des dégâts très réduits
                dialogue.text = "You blocked the attack.";
                yield return new WaitForSeconds(2);

                NextTurn();
            }

            else if (enemyAction == Action.BLOCK)
            {
                //Rien
                dialogue.text = "You and the enemy are blocking...";
                yield return new WaitForSeconds(2);

                NextTurn();
            }

            else if (enemyAction == Action.DODGE)
            {
                //L'ennemi contourne la garde du joueur
                int damage = enemyHealth.stats.atk - (playerHealth.stats.def / 2);
                //On test l'avantage type
                if (enemyTypeAdvantage)
                {
                    damage = damage * 3 / 2;
                }
                else if (allyTypeAdvantage)
                {
                    damage = damage / 3 * 2;
                }

                //Pour éviter de heal l'ennemi
                if (damage < 0)
                {
                    damage = 0;
                }

                if (!playerHealth.TakeDamage(damage))
                {
                    dialogue.text = "The enemy is attacking behind you.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "You took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    NextTurn();
                }

                else
                {
                    dialogue.text = "The enemy is attacking behind you.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "You took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    system.SetState(new LoseState(system));
                    yield break;
                }
            }

            else if (enemyAction == Action.HEAL)
            {
                //L'ennmi se heal tranquille
                enemyHealth.Heal();
                dialogue.text = "The enemy is healing.";
                yield return new WaitForSeconds(2);

                NextTurn();
            }
        }

        else if (playerAction == Action.DODGE)
        {
            if (enemyAction == Action.TARGETATTACK)
            {
                //L'ennemi se prends un contre
                int damage = playerHealth.stats.atk - (enemyHealth.stats.def / 2);
                //On test l'avantage type
                if (allyTypeAdvantage)
                {
                    damage = damage * 3 / 2;
                }
                else if (enemyTypeAdvantage)
                {
                    damage = damage / 3 * 2;
                }

                //Pour éviter de heal l'ennemi
                if (damage < 0)
                {
                    damage = 0;
                }

                if (!enemyHealth.TakeDamage(damage))
                {
                    dialogue.text = "You countered the attack.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "The enemy took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    NextTurn();
                }

                else
                {
                    dialogue.text = "You countered the attack.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "The enemy took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    system.SetState(new WinState(system));
                    yield break;
                }
            }

            else if (enemyAction == Action.AOEATTACK)
            {
                //Le joueur prends l'attaque de plein fouet
                int damage = (enemyHealth.stats.atk * 3 / 4) - (playerHealth.stats.def / 2);
                //On test l'avantage type
                if (enemyTypeAdvantage)
                {
                    damage = damage * 3 / 2;
                }
                else if (allyTypeAdvantage)
                {
                    damage = damage / 3 * 2;
                }

                //Pour éviter de heal l'ennemi
                if (damage < 0)
                {
                    damage = 0;
                }

                if (!playerHealth.TakeDamage(damage))
                {
                    dialogue.text = "You took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    NextTurn();
                }

                else
                {
                    dialogue.text = "You took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    system.SetState(new LoseState(system));
                    yield break;
                }
            }

            else if (enemyAction == Action.BLOCK)
            {
                //La garde ennemi se fait contourner
                int damage = playerHealth.stats.atk - (enemyHealth.stats.def / 2);
                //On test l'avantage type
                if (allyTypeAdvantage)
                {
                    damage = damage * 3 / 2;
                }
                else if (enemyTypeAdvantage)
                {
                    damage = damage / 3 * 2;
                }

                //Pour éviter de heal l'ennemi
                if (damage < 0)
                {
                    damage = 0;
                }

                if (!enemyHealth.TakeDamage(damage))
                {
                    dialogue.text = "The enemy took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    NextTurn();
                }

                else
                {
                    dialogue.text = "The enemy took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    system.SetState(new WinState(system));
                    yield break;
                }
            }

            else if (enemyAction == Action.DODGE)
            {
                //Rien
                dialogue.text = "Dodge battle !";
                yield return new WaitForSeconds(2);

                NextTurn();
            }

            else if (enemyAction == Action.HEAL)
            {
                //L'ennemi se heal tranquille
                enemyHealth.Heal();
                dialogue.text = "The enemy is healing.";
                yield return new WaitForSeconds(2);

                NextTurn();
            }
        }

        else if (playerAction == Action.HEAL)
        {
            if (enemyAction == Action.TARGETATTACK)
            {
                //Le joueur bouffe l'attaque
                int damage = enemyHealth.stats.atk - (playerHealth.stats.def / 2);
                //On test l'avantage type
                if (enemyTypeAdvantage)
                {
                    damage = damage * 3 / 2;
                }
                else if (allyTypeAdvantage)
                {
                    damage = damage / 3 * 2;
                }

                //Pour éviter de heal l'ennemi
                if (damage < 0)
                {
                    damage = 0;
                }

                if (!playerHealth.TakeDamage(damage))
                {
                    dialogue.text = "You tried to heal.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "You took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    NextTurn();
                }

                else
                {
                    dialogue.text = "You tried to heal.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "You took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    system.SetState(new LoseState(system));
                    yield break;
                }
            }

            else if (enemyAction == Action.AOEATTACK)
            {
                //Le joueur bouffe l'attaque
                int damage = (enemyHealth.stats.atk * 3 / 4) - (playerHealth.stats.def / 2);
                //On test l'avantage type
                if (enemyTypeAdvantage)
                {
                    damage = damage * 3 / 2;
                }
                else if (allyTypeAdvantage)
                {
                    damage = damage / 3 * 2;
                }

                //Pour éviter de heal l'ennemi
                if (damage < 0)
                {
                    damage = 0;
                }
                if (!playerHealth.TakeDamage(damage))
                {
                    dialogue.text = "You tried to heal.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "You took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    NextTurn();
                }

                else
                {
                    dialogue.text = "You tried to heal.";
                    yield return new WaitForSeconds(2);
                    dialogue.text = "You took " + damage + " damage.";
                    yield return new WaitForSeconds(2);

                    system.SetState(new LoseState(system));
                    yield break;
                }
            }

            else if (enemyAction == Action.BLOCK)
            {
                //Le joueur se heal tranquille
                playerHealth.Heal();

                dialogue.text = "You feel renewed strength.";
                yield return new WaitForSeconds(2);

                NextTurn();
            }

            else if (enemyAction == Action.DODGE)
            {
                //Le joueur se heal tranquille
                playerHealth.Heal();

                dialogue.text = "You feel renewed strength.";
                yield return new WaitForSeconds(2);

                NextTurn();
            }

            else if (enemyAction == Action.HEAL)
            {
                //Les deux se heal
                playerHealth.Heal();
                enemyHealth.Heal();

                dialogue.text = "Everyone is healing.";
                yield return new WaitForSeconds(2);

                NextTurn();
            }
        }
    }

    public void NextTurn()
    {
        system.dialogue.enabled = false;
        system.actions.SetActive(true);
        system.playerAction = Action.Null;
        system.enemyAction = Action.Null;
        system.SetState(new ChoseActionState(system));
    }
}
