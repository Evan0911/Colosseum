using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class State
{
    protected readonly BattleSystem system;

    public State(BattleSystem _system)
    {
        system = _system;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual IEnumerator EnemyChoseAction()
    {
        yield break;
    }

    public virtual IEnumerator TurnBegin()
    {
        yield break;
    }

    protected void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public virtual void SetPlayerAttack(Action action)
    {
    }
}
