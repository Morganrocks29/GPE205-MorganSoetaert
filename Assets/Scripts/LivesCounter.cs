using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesCounter : MonoBehaviour
{
    [SerializeField]
    private float _liveImageWidth = 0.5856f;

    [SerializeField]
    private int _maxNumOfLives = 3;

    [SerializeField]
    private int _numOfLives = 3;

    private RectTransform _rect;

    //public UnityEvent OutOfLives;

    public int NumOfLives
    {
        get => _numOfLives;
        private set
        {
            if (value <= 0)
            {
             //   OutOfLives?.Invoke();
            }
            _numOfLives = Mathf.Clamp(value, min: 0, max: _maxNumOfLives);
            AdjustImageWidth();
        }
    }

    private void Awake()
    {
        _rect = transform as RectTransform;
        AdjustImageWidth();
    }

    private void AdjustImageWidth()
    {
        _rect.sizeDelta = new Vector2(x: _liveImageWidth * _numOfLives, _rect.sizeDelta.y);
    }

    public void AddLife(int num = 1)
    {
        NumOfLives += num;
    }

    public void RemoveLife(int num = 1)
    {
        NumOfLives -= num;
    }
}
