using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Gun _gun;
    private Player _player;

    private Coroutine _coroutineShoot;
    

    private bool _isPatrol = true;
    private Vector3 _originPosition;
    private float _distansPlayer;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _originPosition = transform.position;
        StartCoroutine(SetDistantionPatrol());

        _gun = GetComponentInChildren<Gun>();  
        _coroutineShoot = null;
        
        
    }
    private void Update()
    {
        if(_player != null) _distansPlayer = Vector3.Distance(_player.transform.position, transform.position);

        if (_distansPlayer <= 5f) _isPatrol = false;
        else _isPatrol = true;      
   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _player = player;
            _coroutineShoot = StartCoroutine(ShootSpawnBullet());
        }
    }

    
    // Патруль
    private IEnumerator SetDistantionPatrol()
    {
        while(true)
        {
            if (_isPatrol)
            {
                if (_coroutineShoot != null) StopCoroutine(_coroutineShoot);
                var random = Random.insideUnitCircle * 5f;
                var x = random.x;
                var z = random.y;

                var randomPoint = new Vector3(x, 0f, z) + _originPosition;
                _agent.SetDestination(randomPoint);

                yield return new WaitForSeconds(3f);
            }
            else 
            {
                if (_player != null)
                {
                    _agent.SetDestination(_player.transform.position);
                    transform.LookAt(_player.transform.position);
                }

                yield return null;
            }
            
        }

    }
    private IEnumerator ShootSpawnBullet()
    {
        while (true)
        {
            if (!_isPatrol)
            {
                _gun.Shoot(_player.transform.position - transform.position);
                yield return new WaitForSecondsRealtime(2f);
                
            }
            else yield return null;
        }
    }
}
