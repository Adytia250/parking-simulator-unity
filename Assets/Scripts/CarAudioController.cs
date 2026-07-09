using UnityEngine;

public class CarAudioController : MonoBehaviour
{
    public AudioSource idleAudio;
    public AudioSource runningAudio;

    public CarController carController;

    void Start()
    {
        idleAudio.loop = true;
        runningAudio.loop = true;

        idleAudio.Play();
        runningAudio.Play();
    }

    void Update()
    {
        float speed =
            carController.GetCurrentSpeed();

        if(speed < 0.5f)
        {
            idleAudio.volume =
                Mathf.Lerp(
                    idleAudio.volume,
                    1f,
                    Time.deltaTime * 5f);

            runningAudio.volume =
                Mathf.Lerp(
                    runningAudio.volume,
                    0f,
                    Time.deltaTime * 5f);
        }
        else
        {
            idleAudio.volume =
                Mathf.Lerp(
                    idleAudio.volume,
                    0.2f,
                    Time.deltaTime * 5f);

            runningAudio.volume =
                Mathf.Lerp(
                    runningAudio.volume,
                    1f,
                    Time.deltaTime * 5f);

            runningAudio.pitch =
                Mathf.Lerp(
                    1f,
                    2f,
                    speed /
                    carController.GetMaxSpeed());
        }
    }
}