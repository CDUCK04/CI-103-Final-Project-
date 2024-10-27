using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBoost : MonoBehaviour
{
    public PlayerStateMachine playerStateMachine;
    public float speedMultiplier = 2f;
    public Slider slider;
    public float rechargeRate = 0.3f;
    public float rechargeDelay = 1.0f;
    private bool speedIncreased = false;
    private bool isRecharging = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && slider.value > 0)
        {
            if (!speedIncreased)
            {
                playerStateMachine.FreeLookMovementSpeed *= speedMultiplier;
                speedIncreased = true;
            }
            slider.value -= Time.deltaTime /7f;
            StopCoroutine("RechargeSlider");
            isRecharging = false;
        }
        else if (slider.value == 0 && speedIncreased)
        {
            playerStateMachine.FreeLookMovementSpeed /= speedMultiplier;
            speedIncreased = false;
        }
        else
        {
            if (speedIncreased)
            {
                playerStateMachine.FreeLookMovementSpeed /= speedMultiplier;
                speedIncreased = false;
            }

            if (slider.value < 1 && !isRecharging)
            {
                StartCoroutine("RechargeSlider");
            }
        }
    }

    IEnumerator RechargeSlider()
    {
        isRecharging = true;
        yield return new WaitForSeconds(rechargeDelay);
        while (slider.value < 1)
        {
            slider.value += Time.deltaTime * rechargeRate;
            yield return null;
        }
        isRecharging = false;
    }
}