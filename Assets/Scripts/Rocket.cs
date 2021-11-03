using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Rocket: MonoBehaviour
    {
        public Button launchBtn;
        
        public GameObject fullParticle;
        public GameObject secondParticle;
        
        public AudioSource audio;
        
        public float power=10;
        public float dragForce= 20;
        public float full = 5;
        
        private Rigidbody _rbd;
        private float _height;
        private float _maxHeight;
        private bool _uncloak;
        private bool _falling;
        private bool _start;
        
        private void Start()
        {
            launchBtn.onClick.AddListener(Launch);
            _rbd = gameObject.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_start)
            {
                if (!_uncloak)
                {
                    BurnFull() ;
                    return;
                }
                if (!_falling) CheckHeight();
            }
        }

        private void CheckHeight()
        {
            _height = transform.position.y;
            if (_maxHeight<=_height)
            {
                _maxHeight = _height;
            }
            else
            {
                Bootstrap.Instance.parachute.ShowMesh();
                _falling = true;
            }
        }

        private void BurnFull()
        {
            if (full>0)
            {
                full -= Time.deltaTime;
                _rbd.AddRelativeForce(Vector3.up*power);
            }
            else
            {
                _rbd.AddRelativeForce(Vector3.zero);
                Bootstrap.Instance.gasCell.Unclock();
                fullParticle.SetActive(false);
                _uncloak = true;
                StartCoroutine(SecondStage());
            }
        }

        IEnumerator SecondStage()
        {
            secondParticle.SetActive(true);
            audio.pitch = 0.1f;
            yield return new WaitForSeconds(2f);
            secondParticle.SetActive(false);
            audio.Stop();
            Turn();
        }

        private void Turn()
        {
            transform.DOLocalRotate(new Vector3(0, 0, 180), 5f).SetEase(Ease.InOutSine).OnComplete(OpenParachute);
        }

        private void OpenParachute()
        {
            Bootstrap.Instance.parachute.ShowMesh();
            _rbd.drag = dragForce;
        }

        private void Launch()
        {
            _start = true;
            audio.Play();
            fullParticle.SetActive(true);
            launchBtn.gameObject.SetActive(false);
        }

        private void OnCollisionEnter(Collision other)
        {
            _rbd.drag = 0;
        }
    }
}