using System.Collections;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    public static PhoneManager Instance;

    public PhoneInteraction[] phones;

    private int currentPhone = 0;

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(15f);

        StartPhone(currentPhone);
    }

    public void PhoneAnswered()
    {
        currentPhone++;

        if (currentPhone >= phones.Length)
        {
            Debug.Log("Todos os telefones foram atendidos.");
            return;
        }

        StartCoroutine(StartNextPhone());
    }

    private IEnumerator StartNextPhone()
    {
        yield return new WaitForSeconds(5f);

        StartPhone(currentPhone);
    }

    private void StartPhone(int index)
    {
        phones[index].StartRinging();
    }
}